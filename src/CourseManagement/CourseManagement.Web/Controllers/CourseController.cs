using CourseManagement.Domain.Exceptions;
using CourseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public ILogger<CourseController> _logger;
        public CourseController(IServiceScopeFactory serviceScopeFactory,ILogger<CourseController>logger) 
        {
            _scopeFactory = serviceScopeFactory;
            _logger = logger;
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
                try
                {
                    oCourseVM.Resolve(_scopeFactory);
                    await oCourseVM.CreateCourseAsync();
                    TempData["success"] = "Data Saved Successfully";

                    return RedirectToAction("ViewCourses");
                }
                catch(DuplicateTitleException ex)
                {
                    TempData["error"] = ex.Message;

                }
                catch (Exception ex)
                {
                    TempData["error"] = "There was a Problem in Creating Course";
                    _logger.LogError(ex.Message);
                }

            }
            return View(oCourseVM);
        }
    }
}
