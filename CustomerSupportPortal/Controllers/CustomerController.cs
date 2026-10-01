using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportPortal.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashBoard()
        {

            return View();
        }
    }
}
