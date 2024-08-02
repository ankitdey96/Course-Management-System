using CourseManagement.Application.Interfaces;
using CourseManagement.Infrastructure.Membership;
using Microsoft.AspNetCore.Identity;

namespace CourseManagement.Web.Models
{
    public class AccountVM
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }  

        public string Role {  get; set; }
        private UserManager<ApplicationUser> _userManager;
        private IServiceScopeFactory ServiceScope { get; set; }

        public AccountVM()
        {

        }

        public AccountVM(UserManager<ApplicationUser>userManager)
        {
            _userManager = userManager;
        }

        public void Resolve(IServiceScopeFactory serviceScope)
        {
            ServiceScope = serviceScope;
            _userManager = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        public async Task<IList<AccountVM>> GetAllUsers()
        {
            var oUsers =await _userManager.GetUsersInRoleAsync("Student");
            List<AccountVM>oAccountVMs = oUsers.Select(x => new AccountVM
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Role = "Student"
            }).ToList();
            return oAccountVMs;
        }

    }
}
