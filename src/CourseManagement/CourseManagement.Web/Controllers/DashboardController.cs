using CourseManagement.Infrastructure.Membership;
using CourseManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public ILogger<DashboardController> _logger;
        public UserManager<ApplicationUser> _userManager;

        public DashboardController(IServiceScopeFactory serviceScopeFactory, ILogger<DashboardController> logger, UserManager<ApplicationUser> userManager)
        {
            _scopeFactory = serviceScopeFactory;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var oCourseEnrollmentVm = Activator.CreateInstance<CourseEnrollMentVM>();
            oCourseEnrollmentVm.Resolve(_scopeFactory);
            var data = await oCourseEnrollmentVm.GetCourseEnrollments();
            return View(data);
        }
    }
}
