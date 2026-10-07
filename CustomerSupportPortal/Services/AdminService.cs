using CustomerSupportPortal.Models;
using CustomerSupportPortal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportPortal.Services
{
    public class AdminService:IAdminService
    {
        private readonly IAdminRepository _adminRepository;
       
       public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public List<UserProfile> GetAgents()
        {
            return _adminRepository.GetAgents();
        }
    }
}
