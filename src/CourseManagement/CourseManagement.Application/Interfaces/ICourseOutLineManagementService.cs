using CourseManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Interfaces
{
    public interface ICourseOutLineManagementService
    {
        Task CreateCourseOutline(Guid courseID, string topicName, List<CourseTopicDetail> courseTopicDetails);
    }
}
