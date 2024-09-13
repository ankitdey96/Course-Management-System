using AutoMapper;
using CourseManagement.Application.DTOs;
using CourseManagement.Application.Interfaces;
using CourseManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagement.Web.Models
{
    public class CourseEnrollMentVM
    {
        private IServiceScopeFactory _serviceScope { get; set; }
        private ICourseEnrollmentService _courseEnrollmentService { get; set; }
        private IMapper _mapper { get; set; }
        public Guid CourseID { get; set; }
        public Guid StudentID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string CourseName {  get; set; }
        public string StudentName {  get; set; }
        public int TotalEnrollment {  get; set; }
        public int TotalDisplay {  get; set; }
        public CourseEnrollMentVM()
        {

        }

        public CourseEnrollMentVM(ICourseEnrollmentService courseEnrollmentService, IMapper mapper)
        {
            _courseEnrollmentService = courseEnrollmentService;
            _mapper = mapper;
        }

        public void Resolve(IServiceScopeFactory serviceScope)
        {
            _serviceScope = serviceScope;
            _courseEnrollmentService = _serviceScope.CreateScope().ServiceProvider.GetRequiredService<ICourseEnrollmentService>();
            _mapper = _serviceScope.CreateScope().ServiceProvider.GetRequiredService<IMapper>();
        }

        public Task EnrollCourse(Guid CourseID, Guid StudentID)
        {
            return _courseEnrollmentService.EnrollCourse(CourseID,StudentID);
        }

        public async Task<IList<CourseEnrollMentVM>> GetCourseEnrollments()
        {
            var data = await _courseEnrollmentService.GetCourseEnrollment();
            List<CourseEnrollMentVM>courseEnrollMentVMs = new List<CourseEnrollMentVM>();

            foreach(var oItem in data.TotalEnrollments)
            {
                CourseEnrollMentVM oCourseEnrollmentVM = new CourseEnrollMentVM();
                _mapper.Map(oItem, oCourseEnrollmentVM);
                courseEnrollMentVMs.Add(oCourseEnrollmentVM);

            }
            return courseEnrollMentVMs;
        }
    }
}
