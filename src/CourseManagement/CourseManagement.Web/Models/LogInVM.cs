using CourseManagement.Infrastructure.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
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
        private UserManager<ApplicationUser> _userManager;
        private IServiceScopeFactory ServiceScopeFactory { get; set; }

        public LogInVM()
        {

        }

        public LogInVM(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser>userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void Resolve(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            _signInManager = ServiceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
            _userManager = ServiceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        }

        public async Task<(string ?ErrorMessage,string ?RoleName)>LogIn()
        {

            var oResult = await _signInManager.PasswordSignInAsync(Email, Password, RememberMe, false);
            if (oResult.Succeeded)
            {
                return (null, await GetRole(Email));
            }
            else
            {
                return ("Invalid Login Attempt", null);
            }
        }

        public async Task<string> GetRole(string Email)
        {
            var oUser = await _userManager.FindByEmailAsync(Email);
            if (await _userManager.IsInRoleAsync(oUser, "Admin"))
            {
                return "Admin";
            }
            if(await _userManager.IsInRoleAsync(oUser, "Teacher"))
            {
                return "Teacher";
            }
            else
            {
                return "Student";
            }
        }


        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
