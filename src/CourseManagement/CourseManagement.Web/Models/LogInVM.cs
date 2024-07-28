using CourseManagement.Infrastructure.Membership;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Web.Models
{
    public class LogInVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
        private SignInManager<ApplicationUser> _signInManager;
        private IServiceScopeFactory ServiceScopeFactory { get; set; }

        public LogInVM()
        {

        }

        public LogInVM(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void Resolve(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            _signInManager = ServiceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
        }

        public async Task<(string ?ErrorMessage,string ?RedirectUrl)>LogIn()
        {
            var oUser = await _signInManager.PasswordSignInAsync(Email, Password, RememberMe, false);
            if (oUser.Succeeded)
            {
                return (null, ReturnUrl);
            }
            else
            {
                return ("Invalid Login Attempt", null);
            }
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
