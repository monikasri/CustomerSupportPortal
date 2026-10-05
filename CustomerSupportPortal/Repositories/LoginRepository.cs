using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Repositories
{

    public class LoginRepository : ILoginRepository
    {
        private readonly CustomerSupportContext _context;
        public LoginRepository(CustomerSupportContext context)
        {
            _context = context;
        }

        public UserRole? GetRoleNameByID(int? RoleId)
        {
         return _context.UserRoles.FirstOrDefault(x=>x.RoleId== RoleId);
        }
        public UserProfile? ValidateUser(string email, string password)
        {
            return _context.UserProfiles.FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
