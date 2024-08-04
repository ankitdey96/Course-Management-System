using CourseManagement.Application.Interfaces;
using CourseManagement.Infrastructure.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Web.Models
{
    public class AccountVM
    {
        public string UserName {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }  

        public string Role {  get; set; }

        public Guid RoleId {  get; set; }
        private UserManager<ApplicationUser> _userManager;

        private RoleManager<ApplicationRole> _roleManager;

        public List<SelectListItem> RolesList { get; set; }
        private IServiceScopeFactory ServiceScope { get; set; }

        public AccountVM()
        {

        }

        public AccountVM(UserManager<ApplicationUser>userManager,RoleManager<ApplicationRole>roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Resolve(IServiceScopeFactory serviceScope)
        {
            ServiceScope = serviceScope;
            _userManager = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        }

        public async Task<IList<AccountVM>> GetAllUsers()
        {
            List<AccountVM> oAccountVMs = new List<AccountVM>();
            var oUsers =await _userManager.GetUsersInRoleAsync("Student");

            foreach(var ouser in oUsers)
            {
                var oUserRoleList = await _userManager.GetRolesAsync(ouser);
                foreach(var orolename in oUserRoleList)
                {
                    var oRole = _roleManager.FindByNameAsync(orolename);
                    oAccountVMs.Add(new AccountVM{
                        FirstName = ouser.FirstName,
                        LastName = ouser.LastName,
                        UserName = ouser.UserName ?? string.Empty,
                        Role = oRole.Result.Name,
                        RoleId = oRole.Result.Id,
                        Email = ouser.Email,
                    });
                }
            }

            return oAccountVMs;
        }

        public async Task<List<SelectListItem>> GetAllRoles()
        {
            var oRoles = await _roleManager.Roles.Where(x => x.Name != "Admin").ToListAsync();
            var oRoleList = oRoles.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            return oRoleList;
        }
    }
}
