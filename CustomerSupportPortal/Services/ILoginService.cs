using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Services
{
    public interface ILoginService
    {
        Task<bool> AuthenticateUserAsync(string email, string password);

    }
}
