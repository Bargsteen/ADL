using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ADL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<Person> userManager;
        private SignInManager<Person> signInManager;
        private ISchoolRepository schoolRepository;
        public AccountController(UserManager<Person> userMgr,
                SignInManager<Person> signinMgr,
                ISchoolRepository schoolRepo)
        {
            schoolRepository = schoolRepo;
            userManager = userMgr;
            signInManager = signinMgr;
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
            AvailableSchools = schoolRepository.Schools
        });

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                School school = schoolRepository.Schools.FirstOrDefault(s => s.SchoolId == model.SchoolId);
                Person user = new Person
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    PersonType = model.PersonType,
                    School = school,
                    UserName = model.Username,
                    Email = model.Email,
                };
                IdentityResult result
                    = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    TempData["message"] = $"Brugeren '{model.Username}' blev oprettet";
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
