using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Web.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult ViewCourses()
        {
            return View();
        }
    }
}
