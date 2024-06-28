using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Repositories;

namespace CourseManagement.Application.Services
{
    public class CourseManagementService : ICourseManagementService
    {
        private readonly IUnitOfWork _unitOfWork; 
        public CourseManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateCourse(string Name, string Description, Guid TeacherID, int NoOfClasses, decimal Fees)
        {
            bool IsDuplicateTitle =await _unitOfWork.CourseRepository.IsDuplicate(x => x.Name == Name);

            Course oCourse = new Course
            {
                Name = Name,
                Description = Description,
                TeacherId = TeacherID,
                NoOfClasses = NoOfClasses,
                Fees = Fees
            };

            await _unitOfWork.CourseRepository.AddAsync(oCourse);
            await _unitOfWork.SaveAsync();
        }
    }
}
