using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffHub.Entities.IdentityEntities;
using StaffHub.ServiceContracts.DTO;
using StaffHub.ServiceContracts.Enums;

namespace StaffHub.Controllers
{
    [Route("account")]
    //[AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
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

                if (registerDTO.UserType == UserTypeOptions.Admin)
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserTypeOptions.Admin.ToString()
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                }
                else
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserTypeOptions.User.ToString()
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                }
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
        [Authorize("NotAuthenticated")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        [Authorize("NotAuthenticated")]
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
                ApplicationUser user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user != null)
                {
                    return RedirectToAction(nameof(EmployeeController.Index), "Employee");
                    //if (await _userManager.IsInRoleAsync(user, UserTypeOptions.Admin.ToString()))
                    //{
                    //    return RedirectToAction("Index", "Home", new {area = "admin"});
                    //}
                }
                
                
            }
            ModelState.AddModelError("Login", "Invalid email or password");
            return View();
        }

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(EmployeeController.Index), "Employee");
        }
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true); //valid email
            }
            else
            {
                return Json(false); //email already in use
            }
        }
    }
}
