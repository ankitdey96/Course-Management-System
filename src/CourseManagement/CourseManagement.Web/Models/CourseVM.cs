using AutoMapper;
using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Web.Models
{
    public class CourseVM
    {
        private IServiceScopeFactory ServiceScope { get; set; }
        private IMapper _mapper { get; set; }
        public ICourseManagementService _courseManagementService;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
        public int NoOfClasses { get; set; }
        public decimal Fees { get; set; }
        public List<CourseTopic> CourseTopics { get; set; }

        public int PageNo {  get; set; }
        public List<SelectListItem>? TeacherList { get; set; }
        public byte[] ?Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public int PageSize {  get; set; }

        public User? Teacher { get; set; }
        public CourseVM() 
        {

        }

        public CourseVM(ICourseManagementService courseManagementService,IMapper mapper)
        {
            _courseManagementService = courseManagementService;
            _mapper = mapper;
        }

        public void Resolve(IServiceScopeFactory serviceScope)
        {
            ServiceScope = serviceScope;
            _courseManagementService = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<ICourseManagementService>();
            _mapper = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<IMapper>();
        }

        public async Task CreateAsync()
        {
           if(ImageFile is not null)
           {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    Image = memoryStream.ToArray();
                }
            }
           await _courseManagementService.CreateCourse(Name,Description,TeacherId,NoOfClasses,Fees,Image);
        }

        public async Task<IList<Course>> GetCourseWithPaginationAsync()
        {
            var data = await  _courseManagementService.GetPagedCourseAsync(PageNo,PageSize);

            return data;

        }

        public async Task<CourseVM> GetCoure(Guid id)
        {
            Course oCourse =  await _courseManagementService.GetCourseByID(id);
            CourseVM oCourseVM = new CourseVM();
            if (oCourse is not null)
            {
                _mapper.Map(oCourse, oCourseVM);
            }

            return oCourseVM;
        }

        public async Task UpdateAsync()
        {
            if (ImageFile is not null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    Image = memoryStream.ToArray();
                }
            }

            await _courseManagementService.UpdateCourseAsync(Id,Name,Description,NoOfClasses,Fees,TeacherId,Image);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _courseManagementService.DeleteCourseAsync(id);
        }

        public async Task<List<CourseVM>> GetAllCoursesAsync()
        {
            var oCourse = await _courseManagementService.GetAllCourses();
            List<CourseVM> oCourseVMs = new List<CourseVM>();

            oCourseVMs = _mapper.Map<List<CourseVM>>(oCourse);

            return oCourseVMs;
        }

        public async Task<IList<Course>> GetAssignedCourseOfTeacher(string TeacherID)
        {
            Guid LogINTeacherID = Guid.Parse(TeacherID);

            return await _courseManagementService.GetAssignedCourse(LogINTeacherID);

        }
    }
}
