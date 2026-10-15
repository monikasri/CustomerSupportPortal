using CustomerSupportPortal.Models;
using CustomerSupportPortal.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CustomerSupportPortal.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;
        public LoginService(ILoginRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
        }
        public UserModel? AuthenticateUserAsync(string username, string password)
        {
            UserModel? usersModel = _repository.ValidateUser(username, password);
            return usersModel;
        }
    }
}
