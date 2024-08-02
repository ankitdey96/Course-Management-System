using CourseManagement.Application.Interfaces;
using CourseManagement.Application.Services;
using CourseManagement.Infrastructure.Membership;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Web.Models
{
    public class RegisterVM
    {
        private IServiceScopeFactory ServiceScope { get; set; }
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string? ReturnUrl { get; set; }
        public RegisterVM()
        {

        }

        public RegisterVM(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void Resolve(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScope = serviceScopeFactory;
            _signInManager = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
            _userManager = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        
       

        public async Task<(IEnumerable<IdentityError>?errors,string ?RedirectLocation)> Registration(string RedirectLocation)
        {
            var oUser = new ApplicationUser
            {
                UserName = Email,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };
            var oResult = await _userManager.CreateAsync(oUser, Password);
            if (oResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(oUser,"Student");
                await _signInManager.SignInAsync(oUser, false);
                return (null, RedirectLocation);
            }
            else
            {
                return (oResult.Errors, null);
            }
        }

    }
}
