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

        public string ErrorMessage { get; set; } = string.Empty;

        //kiểm tra người dùng đã login chưa
        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("/Rooms/Index");
            }
            return Page();
        }

        //Login
        public async Task<IActionResult> OnPostAsync()
        {
            //chặn login nếu ko nhập gì
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Email and password are required!";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            // gán account bằng Email Password
            var Account = await _userService.Login(Email, Password);

            //chặn login nếu chỉ nhập 1 trong 2
            if (Account == null)
            {
                ErrorMessage = "Invalid Email or Password! Please try again.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            //Cookie
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Account.FullName.ToString()),
                new Claim(ClaimTypes.Role, Account.Role.ToString()),
                new Claim(ClaimTypes.GivenName, Account.CampusId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenProperties = new AuthenticationProperties
            {
                IsPersistent = false
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authenProperties
            );

            //Chuyển hướng trang khi đăng nhập thành công
            return RedirectToPage("/Rooms/Index");

        }
    }
}
