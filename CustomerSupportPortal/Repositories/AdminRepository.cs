using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Repositories
{
    public class AdminRepository:IAdminRepository
    {
        private readonly CustomerSupportContext _context;
        public AdminRepository(CustomerSupportContext context)
        {
            _context = context;
        }
        public List<UserProfile> GetAgents()
        {
            return _context.UserProfiles
                .Where(x => x.Role.RoleName == "Agent")
                .Select(x => new UserProfile
                {
                    UserId = x.UserId,
                    UserName = x.UserName
                })
                .ToList();
        }
    }
}
