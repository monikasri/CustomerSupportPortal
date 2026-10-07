using CustomerSupportPortal.Models;
using CustomerSupportPortal.Repositories;
using CustomerSupportPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CustomerSupportPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ISupportRequestRepository _supportService;
        public AdminController(IAdminService adminService, ISupportRequestRepository supportService)
        {
            _adminService = adminService;
            _supportService = supportService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminDashBoard()
        {
            var userName = User.Identity?.Name;

            return View();
        }
        [HttpGet]
        public IActionResult GetAgents()
        {
            var agentList = _adminService.GetAgents();
            return Json(agentList);
        }
        [HttpPost]
        public IActionResult GetAllRequest(int start, int length)
        {
            string search = Request.Form["search[value]"].ToString();
            var result = _supportService.GetAllRequests(start, length, search);
            var sequenceNumber = Request.Form["draw"].ToString();
            return Json(new
            {
                draw = sequenceNumber,
                recordsTotal = result.TotalRecords,
                recordsFiltered = result.TotalRecords,
                data = result.Data
            });
        }
    }
}
