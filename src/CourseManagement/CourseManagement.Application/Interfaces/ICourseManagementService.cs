namespace CourseManagement.Application.Interfaces;

public interface ICourseManagementService
{
    Task CreateCourse(string Name, string Description, Guid TeacherID, int NoOfClasses, decimal Fees);
}
