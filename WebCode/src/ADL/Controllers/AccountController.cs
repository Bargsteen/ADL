using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using ADL.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using static ADL.Models.EnumCollection;

namespace ADL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<Person> userManager;
        private SignInManager<Person> signInManager;
        private IUserValidator<Person> userValidator;
        private IPasswordValidator<Person> passwordValidator; 
        private IPasswordHasher<Person> passwordHasher;
        private ISchoolRepository schoolRepository;

        private IClassRepository classRepository;
        public AccountController(UserManager<Person> userMgr,
                SignInManager<Person> signinMgr,
                ISchoolRepository schoolRepo, 
                IClassRepository classRepo,
                IUserValidator<Person> userValid,
                IPasswordValidator<Person> passValid,
                IPasswordHasher<Person> passwordHash)
        {
            schoolRepository = schoolRepo;
            classRepository = classRepo;
            userManager = userMgr;
            signInManager = signinMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Person user = await userManager.FindByNameAsync(details.Username);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(
                                user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Username),
                    "Ugyldig brugernavn eller ugyldigt kodeord.");
            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public ViewResult Create() => View(new CreateModel()
        {
            AvailableSchools = schoolRepository.Schools,
            AvailableClasses = classRepository.Classes
        });

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                Person user = new Person
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    PersonType = model.PersonType,
                    SchoolId = model.SchoolId,
                    UserName = model.Username,
                    Email = model.Email,
                };
                IdentityResult result
                    = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if(model.ClassId != 0)
                    {
                        Person newUserDbEntry = await userManager.FindByNameAsync(user.UserName); // It needs the actual db entry
                        classRepository.AddPersonToClass(model.ClassId, newUserDbEntry);
                    }
                    TempData["message"] = $"Brugeren '{model.Username}' blev oprettet";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            TempData["errorMessage"] = "Der skete en fejl. Prøv igen";
            model.AvailableClasses = classRepository.Classes;
            model.AvailableSchools = schoolRepository.Schools;
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            Person user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditModel model)
        {
            Person user = await userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;
                IdentityResult validEmail
                    = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager,
                    user, model.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user,
                        model.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null)
                        || (validEmail.Succeeded
                        && model.Password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

    }
}
