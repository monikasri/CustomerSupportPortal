using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Repositories
{
    public interface ILoginRepository
    {
        UserModel? ValidateUser(string email, string password);
    }
}
