using Azure;
using CourseManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public ILogger<AccountController> _logger;

        public AccountController(IServiceScopeFactory serviceScopeFactory,ILogger<AccountController>logger)
        {
            _scopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public IActionResult Register()
        {
            var oRegisterVM =Activator.CreateInstance<RegisterVM>();
            return View(oRegisterVM);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult>Register(RegisterVM oRegisterVM)
        {
            if (ModelState.IsValid)
            {
                string RedirectUrl = oRegisterVM.ReturnUrl ?? Url.Content("~/");
                oRegisterVM.Resolve(_scopeFactory);
                var oUser = await oRegisterVM.Registration(RedirectUrl);
                if(oUser.errors is not null)
                {
                    foreach (var error in oUser.errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    return Redirect(oUser.RedirectLocation);
                }

            }

            return View(oRegisterVM);
        }

        public IActionResult Login()
        {
            var oLoginVM = Activator.CreateInstance<LogInVM>();
            return View(oLoginVM);
        }
    }
}
