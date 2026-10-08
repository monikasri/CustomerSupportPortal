using CustomerSupportPortal.Models;
using Microsoft.AspNetCore.Identity;
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
            return _context.UserRoles.FirstOrDefault(x => x.RoleId == RoleId);
        }
        public UserModel? ValidateUser(string email, string password)
        {
            var userDetails = _context.UserProfiles
                    .Where(x => x.Email == email && x.Password == password)
                    .Select(x => new UserModel
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        Email = x.Email,
                        RoleId = x.Role.RoleId,
                        RoleName = x.Role.RoleName
                    }).FirstOrDefault(); ;
            return userDetails;
        }
        public int? GetUserIdByName(string userName)
        {
            return _context.UserProfiles.FirstOrDefault(x => x.UserName == userName)?.UserId;
        }
    }
}
