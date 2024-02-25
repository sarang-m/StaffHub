using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffHub.Entities.IdentityEntities;
using StaffHub.ServiceContracts.DTO;

namespace StaffHub.Controllers
{
    [Route("account")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            //check validation errors
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors)
                    .Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }

            ApplicationUser user = new ApplicationUser() { 
                Email = registerDTO.Email, 
                UserName = registerDTO.Email,
                EmployeeName = registerDTO.EmployeeName,
            };

            IdentityResult identityResult = await _userManager.CreateAsync(user,registerDTO.Password);
            if (identityResult.Succeeded)
            {
                //Sign in
                await _signInManager.SignInAsync(user, isPersistent:false);
                return RedirectToAction(nameof(EmployeeController.Index), "Employee");
            }
            else
            {
                foreach(IdentityError identityError  in identityResult.Errors)
                {
                    ModelState.AddModelError("Register", identityError.Description);

                }
                return View(registerDTO);
            }

        }


        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors)
                    .Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }
            var result = await _signInManager.
                PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
                isPersistent:false, lockoutOnFailure:false);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(EmployeeController.Index), "Employee");
            }
            ModelState.AddModelError("Login", "Invalid email or password");
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(EmployeeController.Index), "Employee");
        }
    }
}
