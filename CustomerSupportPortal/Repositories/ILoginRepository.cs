using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Repositories
{
    public interface ILoginRepository
    {
        UserProfile? ValidateUser(string email, string password);
        UserRole? GetRoleNameByID(int? RoleId);
    }
}
