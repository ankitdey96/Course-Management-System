using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int PageNo {  get; set; }
        public List<SelectListItem>? TeacherList { get; set; }
        public byte[] ?Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public int PageSize {  get; set; }
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
           await _courseManagementService.CreateCourse(Name,Description,Guid.Empty,NoOfClasses,Fees,Image);
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
                oCourseVM = new CourseVM
                {
                    Name = oCourse.Name,
                    Description = oCourse.Description,
                    TeacherId = oCourse.TeacherId,
                    NoOfClasses = oCourse.NoOfClasses,
                    Fees = oCourse.Fees,
                };

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

        
    }
}
