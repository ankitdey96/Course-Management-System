using CourseManagement.Domain.Entities;
using System.Linq.Expressions;

namespace CourseManagement.Application.Interfaces;

public interface ICourseManagementService
{
    Task CreateCourse(string Name, string Description, Guid TeacherID, int NoOfClasses, decimal Fees);
    Task<IList<Course>> GetPagedCourseAsync(int pageNo , int pageSize = 10,
             Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null);
}
