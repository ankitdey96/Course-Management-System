﻿using Azure;
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

        public async Task<IActionResult> AssignRole()
        {
            AccountVM oAccountVM = Activator.CreateInstance<AccountVM>();
            oAccountVM.Resolve(_scopeFactory);
            var oUsers = await oAccountVM.GetAllUsers();

            return View(oUsers);
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
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInVM oLogInVM)
        {
            if (ModelState.IsValid)
            {
                oLogInVM.Resolve(_scopeFactory);
                var oUser  = await oLogInVM.LogIn();
                if(oUser.ErrorMessage is not null)
                {
                    ModelState.AddModelError(string.Empty,oUser.ErrorMessage);
                }
                else
                {
                    return Redirect(oUser.RedirectUrl);
                }
            }

            return View(oLogInVM);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            LogInVM oLogInVM = Activator.CreateInstance<LogInVM>();
            oLogInVM.Resolve(_scopeFactory);
            await oLogInVM.LogOut();

            if(returnUrl is not null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
