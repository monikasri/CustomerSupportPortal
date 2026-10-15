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
        /// <summary>
        /// Authenticate User Based on Email and Password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
    }
}
