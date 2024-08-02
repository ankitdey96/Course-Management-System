using Microsoft.AspNetCore.Identity;

namespace CourseManagement.Infrastructure.Membership
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
