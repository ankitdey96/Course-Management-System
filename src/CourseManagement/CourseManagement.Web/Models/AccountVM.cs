using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Exceptions;
using CourseManagement.Infrastructure.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace CourseManagement.Web.Models
{
    public class AccountVM
    {
        public Guid ?UserID {  get; set; }
        public string ?UserName {  get; set; }
        public string ?FirstName {  get; set; }
        public string ?LastName { get; set; }

        public string ?Email { get; set; }  

        public string ?Role {  get; set; }

        public Guid ?RoleId {  get; set; }
        private UserManager<ApplicationUser> _userManager;

        private RoleManager<ApplicationRole> _roleManager;

        public List<SelectListItem> ?RolesList { get; set; }
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
            IList<ApplicationUser> oUsers = await _userManager.GetUsersInRoleAsync("Student");
            oUsers.AddRange(await _userManager.GetUsersInRoleAsync("Teacher"));

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
                        UserID = ouser.Id
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

        public async Task<IEnumerable<IdentityError>> EditRole()
        {
            var oUser = await _userManager.FindByNameAsync(UserName);
            if(oUser is null)
            {
                throw new NotFoundException("User");
            }

            if(!await _roleManager.RoleExistsAsync(Role))
            {
                throw new NotFoundException("Role");
            }
            var oCurrentRole = await _userManager.GetRolesAsync(oUser);
            foreach(var oRole in oCurrentRole)
            {
                var oResult = await _userManager.RemoveFromRoleAsync(oUser, oRole);
                if (!oResult.Succeeded)
                {
                    return oResult.Errors;
                }
            }

            var oNewRole = await _userManager.AddToRoleAsync(oUser, Role);
            if (!oNewRole.Succeeded)
            {
                return oNewRole.Errors;
            }
            return [];
        }

        public async Task<List<SelectListItem>?> GetTeacherList()
        {
            var oRoles = await _userManager.GetUsersInRoleAsync("Teacher");
            var oRoleList = oRoles.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.FirstName + " " + s.LastName,
            }).ToList();

            return oRoleList;
        }
    }
}
