using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Services
{
    public interface ILoginService
    {
        UserProfile? AuthenticateUserAsync(string email, string password);

    }
}
