using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.RoomBooking.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PRN222.RoomBooking.UserRazor.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Rooms/Index");
            }
            ReturnUrl = returnUrl ?? "/Rooms/Index";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? "/Rooms/Index";

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Email and password are required!";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            var Account = await _userService.Login(Email, Password);

            if (Account == null)
            {
                ErrorMessage = "Invalid Email or Password! Please try again.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            if (Account.Role != "User")
            {
                ErrorMessage = "Only users with the 'User' role are allowed to log in.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            // Store both FullName and UserCode in claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Account.FullName.ToString()), 
                new Claim(ClaimTypes.NameIdentifier, Account.UserCode.ToString()),
                new Claim(ClaimTypes.Role, Account.Role.ToString()),
                new Claim(ClaimTypes.GivenName, Account.Campus?.CampusName ?? "Unknown Campus")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authenProperties
            );

            return LocalRedirect(ReturnUrl);
        }
    }
}