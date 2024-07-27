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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogInVM()
        {

        }

        public LogInVM(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }


    }
}
