using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Services
{
    public interface IAdminService
    {
        List<UserProfile> GetAgents();
    }
}
