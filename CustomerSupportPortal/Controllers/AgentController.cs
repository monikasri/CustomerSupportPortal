using CustomerSupportPortal.Models;
using CustomerSupportPortal.Repositories;
using CustomerSupportPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomerSupportPortal.Controllers
{
    [Authorize(Roles = "Agent")]
    public class AgentController : Controller
    {
        private readonly ISupportRequestRepository _supportService;

        public AgentController(ISupportRequestRepository supportService)
        {
            _supportService = supportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AgentDashboard()
        {
            var userName = User.Identity?.Name;
            return View();
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

        [HttpGet]
        public IActionResult GetPriorities()
        {
            var resultPriority = _supportService.GetPriorities();
            return Json(resultPriority);
        }
    }
}
