using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Exceptions;
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

            if (IsDuplicateTitle)
                throw new DuplicateTitleException();
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

       
        public async Task<IList<Course>> GetPagedCourseAsync(int pageNo, int pageSize = 10, Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null)
        {
            if (orderBy is null)
                orderBy = query => query.OrderBy(x => x.Name);

            return await _unitOfWork.CourseRepository.GetPaginateList(pageNo, pageSize,null,null,orderBy);
        }
    }
}
