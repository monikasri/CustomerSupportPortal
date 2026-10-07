
using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Repositories
{
    public interface IAdminRepository
    {
        List<UserProfile> GetAgents();
    }
}
