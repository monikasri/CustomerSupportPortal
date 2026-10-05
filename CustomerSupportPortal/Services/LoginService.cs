using CustomerSupportPortal.Models;
using CustomerSupportPortal.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CustomerSupportPortal.Services
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoginRepository _repository;
        public LoginService(ILoginRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            UserProfile? usersProfile = _repository.ValidateUser(username, password);
            if (usersProfile != null)
            {
                UserRole? role = _repository.GetRoleNameByID(usersProfile.RoleId);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usersProfile.UserName),
                    new Claim(ClaimTypes.Role,role.RoleName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
