using Microsoft.AspNetCore.Mvc;
using CustomerSupportPortal.Services;
using CustomerSupportPortal.Repositories;
namespace CustomerSupportPortal.Controllers
{
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
            string search = Request.Form["search[value]"].ToString();
            var result = _supportService.GetAllRequests(start, length, search);
            var draw = Request.Form["draw"].ToString();
            
            //  return Json(returnList);
            return Json(new
            {
                draw = draw,
                recordsTotal = result.TotalRecords,
                recordsFiltered = result.TotalRecords,
                data = result.Data
            });
        }
    }
}
