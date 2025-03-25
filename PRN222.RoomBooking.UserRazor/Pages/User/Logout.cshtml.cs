using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN222.RoomBooking.UserRazor.Pages.User
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            // Đăng xuất người dùng
            await HttpContext.SignOutAsync();

            // Xóa toàn bộ cookie của ứng dụng
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            // Điều hướng về trang Login
            return RedirectToPage("/User/Login");
        }
    }
}
