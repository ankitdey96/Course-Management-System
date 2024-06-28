using CourseManagement.Application.Interfaces;

namespace CourseManagement.Web.Models
{
    public class CourseVM
    {
        private IServiceScopeFactory ServiceScope { get; set; }
        public ICourseManagementService _courseManagementService;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
        public int NoOfClasses { get; set; }
        public decimal Fees { get; set; }
        public CourseVM() 
        {

        }

        public CourseVM(ICourseManagementService courseManagementService)
        {
            _courseManagementService = courseManagementService;
        }

        public void Resolve(IServiceScopeFactory serviceScope)
        {
            ServiceScope = serviceScope;
            _courseManagementService = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<ICourseManagementService>();
        }

        public async Task CreateCourseAsync()
        {
           await _courseManagementService.CreateCourse(Name,Description,Guid.Empty,NoOfClasses,Fees);
        }



    }
}
