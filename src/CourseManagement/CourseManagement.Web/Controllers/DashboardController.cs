using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
