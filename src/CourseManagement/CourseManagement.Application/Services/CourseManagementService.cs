using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Exceptions;
using CourseManagement.Domain.Repositories;
using System.Linq.Expressions;


namespace CourseManagement.Application.Services
{
    public class CourseManagementService : ICourseManagementService
    {
        private readonly IUnitOfWork _unitOfWork; 
        public CourseManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateCourse(string Name, string Description, Guid TeacherID, int NoOfClasses, decimal Fees, byte[] Image)
        {
            try
            {
                bool IsDuplicateTitle = await _unitOfWork.CourseRepository.IsDuplicate(x => x.Name == Name);

                if (IsDuplicateTitle)
                    throw new DuplicateTitleException();
                Course oCourse = new Course
                {
                    Name = Name,
                    Description = Description,
                    TeacherId = TeacherID,
                    NoOfClasses = NoOfClasses,
                    Fees = Fees,
                    Image = Image
                };

                await _unitOfWork.CourseRepository.AddAsync(oCourse);
                await _unitOfWork.SaveAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            await _unitOfWork.CourseRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Course>> GetAllCourses()
        {
            List<string> include = new List<string>() { "Teacher"};
            return await _unitOfWork.CourseRepository.GetAllAsync(null,include,null);
        }

        public async Task<Course> GetCourseByID(Guid id)
        {
            return await _unitOfWork.CourseRepository.GetByIdAsync(id);
        }

        public async Task<IList<Course>> GetPagedCourseAsync(int pageNo, int pageSize = 10, Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null)
        {
            try
            {
                if (orderBy is null)
                    orderBy = query => query.OrderBy(x => x.Name);
               
                List<string> include = new List<string>() { "Teacher" };
                return await _unitOfWork.CourseRepository.GetPaginateList(pageNo, pageSize, null, include, orderBy);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task UpdateCourseAsync(Guid id, string name, string description, int noOfClasses, decimal fees, Guid TeacherID, byte[] Image)
        {
            bool IsDuplicateTitle = await _unitOfWork.CourseRepository.IsDuplicate(x => x.Name == name && x.Id != id);

            if (IsDuplicateTitle)
                throw new DuplicateTitleException();

            var oCourse = await GetCourseByID(id);
            if(oCourse is not null)
            {
                oCourse.Description = description;
                oCourse.Fees = fees;
                oCourse.NoOfClasses = noOfClasses;
                oCourse.TeacherId = TeacherID;
                oCourse.Image = Image;
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
