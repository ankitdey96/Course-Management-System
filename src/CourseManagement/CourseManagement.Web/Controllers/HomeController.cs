using CourseManagement.Domain.Entities;
using CourseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CourseManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceScopeFactory _scopeFactory;


        public HomeController(ILogger<HomeController> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task<IActionResult> Index()
        {
            var oCourseVM = Activator.CreateInstance<CourseVM>();
            oCourseVM.Resolve(_scopeFactory);
            var oData = await oCourseVM.GetAllCoursesAsync();

            return View(oData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
