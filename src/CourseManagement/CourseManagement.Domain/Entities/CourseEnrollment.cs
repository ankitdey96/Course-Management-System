using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Domain.Entities
{
    public class CourseEnrollment
    {
        public Guid CourseID { get; set; }
        public Guid StudentID { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
