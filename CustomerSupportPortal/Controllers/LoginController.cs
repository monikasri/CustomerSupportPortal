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
            bool isValid = await _loginService.AuthenticateUserAsync(email, password);
            if (isValid)
            {
                return Ok(new { success = true, message = "Login Successful" });
            }
            else
            {
                return Ok(new { success = false, message = "Login failed" });
            }
            #endregion
        }
    }
}
