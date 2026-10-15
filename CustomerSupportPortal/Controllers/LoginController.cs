using CustomerSupportPortal.Models;
using CustomerSupportPortal.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace CustomerSupportPortal.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            #region Authentication Logic 
            var user = _loginService.AuthenticateUserAsync(email, password);
            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid login"
                });
            }
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleName)
                };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            string? redirectUrl;

            if (user.RoleName == "Admin")
                redirectUrl = Url.Action("AdminDashboard", "Admin");
            else if (user.RoleName == "Agent")
                redirectUrl = Url.Action("AgentDashboard", "Agent");
            else
                redirectUrl = Url.Action("Dashboard", "Customer");

            return Json(new
            {
                success = true,
                redirectUrl
            });
            #endregion
        }
    }
}
