using CourseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public CourseController(IServiceScopeFactory serviceScopeFactory) 
        {
            _scopeFactory = serviceScopeFactory;
        }   
        public IActionResult ViewCourses()
        {
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseVM oCourseVM)
        {
            if (ModelState.IsValid)
            {
                oCourseVM.Resolve(_scopeFactory);
                await oCourseVM.CreateCourseAsync();
            }
            return View(oCourseVM);
        }
    }
}
