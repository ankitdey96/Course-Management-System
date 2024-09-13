using CourseManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Interfaces
{
    public interface ICourseEnrollmentService
    {
        Task EnrollCourse(Guid courseID, Guid studentID);
        Task<(IEnumerable<CourseEnrollmentDto> TotalEnrollments, int TotalEnrollment, int TotalDisplay)> GetCourseEnrollment();
    }
}
