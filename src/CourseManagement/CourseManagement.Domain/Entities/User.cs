using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Domain.Entities
{
    public class User:IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string FullName {  get; set; }
        public string Email { get; set; }
        public string PasswordHash {  get; set; }
        public List<Course> Courses {  get; set; }
    }
}
