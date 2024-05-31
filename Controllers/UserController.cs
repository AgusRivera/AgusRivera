using AMVTravels.Models.Entities.Identity;
using AMVTravels.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMVTravels.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User>signInManager, ILogger<UserController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if( model.Password == model.ConfirmPassword)
                {
                    var user = new User
                    {
                        BusinessArea = model.BusinessArea,
                        Email = model.Email,
                        UserName = model.Email
                    };

                    var res = await _userManager.CreateAsync(user, model.Password);

                    if(res.Succeeded)
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return Json(new {success = true});
                    }
                    var errors = string.Join(",", res.Errors.Select(e => e.Description));
                    return Json(new { success = false, message = $"Ocurrió un error en la creación del Usuario: {errors}" });
                }

            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid login attempt." });
                }
            }

            return Json(new { success = false, message = "Model state is invalid." });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Ok();
        }
    }
}

