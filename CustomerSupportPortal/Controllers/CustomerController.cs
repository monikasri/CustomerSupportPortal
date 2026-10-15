using Azure.Core;
using CustomerSupportPortal.Models;
using CustomerSupportPortal.Repositories;
using CustomerSupportPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace CustomerSupportPortal.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ISupportRequestRepository _supportService;
        public CustomerController(ISupportRequestRepository supportService)
        {
            _supportService = supportService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DashBoard()
        {
            var userName = User.Identity?.Name;
            return View();
        }
        [HttpPost]
        public IActionResult GetAllRequest(int start, int length)
        {
            #region Search and Paging
            string search = Request.Form["search[value]"].ToString();
            var sequenceNumber = Request.Form["draw"].ToString();
            #endregion
            var result = _supportService.GetAllRequests(start, length, search);
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
        [HttpPost]
        public IActionResult CreateRequest([FromBody] RequestDetails request)
        {
            var name = User.Identity?.Name;
            var email = User.FindFirstValue(ClaimTypes.Email);
            _supportService.CreateRequest(request,email);
            return Json(new
            {
                success = true
            });
        }
    }
}
