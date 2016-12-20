using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using ADL.Models.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ADL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;
        private readonly IUserValidator<Person> _userValidator;
        private readonly IPasswordValidator<Person> _passwordValidator; 
        private readonly IPasswordHasher<Person> _passwordHasher;
        private readonly ISchoolRepository _schoolRepository;

        private readonly IClassRepository _classRepository;
        public AccountController(UserManager<Person> userMgr,
                SignInManager<Person> signinMgr,
                ISchoolRepository schoolRepo, 
                IClassRepository classRepo,
                IUserValidator<Person> userValid,
                IPasswordValidator<Person> passValid,
                IPasswordHasher<Person> passwordHash)
        {
            _schoolRepository = schoolRepo;
            _classRepository = classRepo;
            _userManager = userMgr;
            _signInManager = signinMgr;
            _userValidator = userValid;
            _passwordValidator = passValid;
            _passwordHasher = passwordHash;
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
                Person user = await _userManager.FindByNameAsync(details.Username);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await _signInManager.PasswordSignInAsync(
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
            await _signInManager.SignOutAsync();
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
            AvailableSchools = _schoolRepository.Schools,
            AvailableClasses = _classRepository.Classes
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
                    Email = model.Email
                };
                IdentityResult result
                    = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if(model.ClassId != 0)
                    {
                        Person newUserDbEntry = await _userManager.FindByNameAsync(user.UserName); // It needs the actual db entry
                        _classRepository.AddPersonToClass(model.ClassId, newUserDbEntry);
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
            model.AvailableClasses = _classRepository.Classes;
            model.AvailableSchools = _schoolRepository.Schools;
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            Person user = await _userManager.FindByIdAsync(id);
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
            Person user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;
                IdentityResult validEmail
                    = await _userValidator.ValidateAsync(_userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager,
                    user, model.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user,
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
                    IdentityResult result = await _userManager.UpdateAsync(user);
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
