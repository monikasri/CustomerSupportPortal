using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Services
{
    public interface ILoginService
    {
        UserModel? AuthenticateUserAsync(string email, string password);

    }
}
