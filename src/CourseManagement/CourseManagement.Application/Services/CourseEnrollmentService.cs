using CourseManagement.Application.DTOs;
using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Services
{
    public class CourseEnrollmentService:ICourseEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseEnrollmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task EnrollCourse(Guid courseID, Guid studentID)
        {
            bool IsDuplicate = await _unitOfWork.CourseEnrollmentRepository.IsDuplicate(x => x.CourseID == courseID && x.StudentID == studentID);
            if (IsDuplicate)
                throw new Exception("Student Is Already Enrolled");
            CourseEnrollment oCourseEnrollment = new CourseEnrollment
            {
                CourseID = courseID,
                StudentID = studentID,
                EnrollmentDate = DateTime.Now
            };

            await _unitOfWork.CourseEnrollmentRepository.AddAsync(oCourseEnrollment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IEnumerable<CourseEnrollmentDto> TotalEnrollments, int TotalEnrollment, int TotalCourses)> GetCourseEnrollment()
        {
            IDictionary<string, object> InputPerameters = new Dictionary<string, object>
            {
                 { "OrderBy",  "CourseName" },
                    { "CourseName", "%"},
                    { "StudentName","%" },
                    { "EnrollmentDateFrom",  null},
                    { "EnrollmentDateTo",  null }
            };

            IDictionary<string, Type> OutParameters = new Dictionary<string, Type>
            {
                    { "TotalEnrollment",  typeof(int)},
                    { "TotalCourses",  typeof(int) }
            };
            var data = await _unitOfWork.DapperUtility.ExecuteStoredProcedure<CourseEnrollmentDto>
                ("GetCourseEnrollments",InputPerameters,OutParameters);
            return (data.result, (int)data.outValues["TotalEnrollment"], (int)data.outValues["TotalCourses"]);
        }
    }
}
