using CourseManagement.Domain.Entities;
using CourseManagement.Infrastructure.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Infrastructure.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid, IdentityUserClaim<Guid>,ApplicationUserRole, IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>,IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedAdminUserWithRole(builder);

            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole[]
                {
                    new ApplicationRole{Name="Admin", Id=new Guid("64347f96-85a1-4326-95f9-8291f9b64611")},
                    new ApplicationRole{Name="Student", Id=Guid.NewGuid()},
                    new ApplicationRole{Name="Teacher", Id=Guid.NewGuid()},
                }
            );
            builder.Entity<ApplicationUserRole>().HasData(
                    new ApplicationUserRole
                    {
                        UserId = new Guid("abc5d607-8ccc-46ef-b56c-c0e8fff6cc8e"),
                        RoleId = new Guid("64347f96-85a1-4326-95f9-8291f9b64611")
                    }
            );

        }

        private void SeedAdminUserWithRole(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            ApplicationUser oAdminUser = new ApplicationUser
            {
                Id = new Guid("abc5d607-8ccc-46ef-b56c-c0e8fff6cc8e"),
                FirstName = "Admin",
                LastName = string.Empty,
                Email = "admin@gmail.com"
            };
            oAdminUser.PasswordHash = hasher.HashPassword(oAdminUser, "Admin@123$");
            builder.Entity<ApplicationUser>().HasData(oAdminUser);


        }
        public DbSet<Course>Course { get; set; }
    }
}
