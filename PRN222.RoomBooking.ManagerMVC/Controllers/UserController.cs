using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PRN222.RoomBooking.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.ManagerMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /User/Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["LoginMessage"] = "Email and Password are required.";
                return View();
            }

            var user = await _userService.Login(email, password);
            if (user == null)
            {
                TempData["LoginMessage"] = "Invalid email or password.";
                return View();
            }

            // Check if the user has the "Manager" role
            if (user.Role != "Manager")
            {
                TempData["LoginMessage"] = "Access denied. Only Managers can log in here.";
                return View();
            }

            // Create claims for the authenticated user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.GivenName, user.Campus?.CampusName ?? "Unknown Campus"),
                new Claim("UserCode", user.UserCode)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // You can make this configurable (e.g., "Remember Me" checkbox)
            };

            // Sign in the user
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["LoginMessage"] = "Login successful!";
            return RedirectToLocal(returnUrl);
        }

        // GET: /User/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return View();
        }

        // POST: /User/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutConfirmed()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}