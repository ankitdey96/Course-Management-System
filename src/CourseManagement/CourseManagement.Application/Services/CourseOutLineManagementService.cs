using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Exceptions;
using CourseManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CourseManagement.Application.Services
{
    public class CourseOutLineManagementService:ICourseOutLineManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseOutLineManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCourseOutline(Guid courseID, string topicName, List<CourseTopicDetail> courseTopicDetails)
        {
            bool IsDuplicateTopic = await _unitOfWork.CourseTopicRepository.IsDuplicate(x => x.TopicName == topicName && x.CourseID == courseID);
            if(IsDuplicateTopic)
            {
                throw new DuplicateTitleException();
            }
            CourseTopic oCourseTopic = new CourseTopic
            {
                TopicName = topicName,
                CourseID = courseID,
                TopicDetails = courseTopicDetails
            };

            await _unitOfWork.CourseTopicRepository.AddAsync(oCourseTopic);
            await _unitOfWork.SaveAsync();
        }
    }
}
