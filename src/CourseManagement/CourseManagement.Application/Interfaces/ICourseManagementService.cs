using CourseManagement.Domain.Entities;
using System.Linq.Expressions;

namespace CourseManagement.Application.Interfaces;

public interface ICourseManagementService
{
    Task CreateCourse(string Name, string Description, Guid TeacherID, int NoOfClasses, decimal Fees, byte[]Image);
    Task DeleteCourseAsync(Guid id);
    Task<List<Course>> GetAllCourses();
    Task<IList<Course>> GetAssignedCourse(Guid logINTeacherID);
    Task<Course> GetCourseByID(Guid id);
    Task<IList<Course>> GetPagedCourseAsync(int pageNo , int pageSize = 10,
             Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null);
    Task UpdateCourseAsync(Guid id, string name, string description, int noOfClasses, decimal fees,Guid TeacherID, byte[] Image);
}
