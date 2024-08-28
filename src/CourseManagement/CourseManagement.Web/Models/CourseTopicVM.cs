using AutoMapper;
using CourseManagement.Application.Interfaces;
using CourseManagement.Application.Services;
using CourseManagement.Domain.Entities;

namespace CourseManagement.Web.Models
{
    public class CourseTopicVM
    {
        public Guid Id { get; set; }
        public Guid CourseID { get; set; }
        public string TopicName { get; set; }
        public Course Course { get; set; }
        private IServiceScopeFactory ServiceScope { get; set; }
        private IMapper _mapper { get; set; }
        public ICourseOutLineManagementService _courseOutLineManagementService;
        public List<CourseTopicDetail> TopicDetails {  get; set; }
        public CourseTopicVM()
        {
        }

        public CourseTopicVM(IMapper mapper,ICourseOutLineManagementService courseOutLineManagementService)
        {
             _courseOutLineManagementService = courseOutLineManagementService;
             _mapper = mapper;

        }

        public void Resolve(IServiceScopeFactory serviceScope)
        {
            ServiceScope = serviceScope;
            _courseOutLineManagementService = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<ICourseOutLineManagementService>();
            _mapper = ServiceScope.CreateScope().ServiceProvider.GetRequiredService<IMapper>();
        }

        public async Task CreateoutLine(List<CourseTopicVM> oCourseTopicVMs)
        {
            foreach(CourseTopicVM oItem in oCourseTopicVMs)
            {
               await  _courseOutLineManagementService.CreateCourseOutline(oItem.CourseID,oItem.TopicName,oItem.TopicDetails);
            }
        }

        public async Task<IList<Course>> GetCourseDetail(Guid courseID)
        {
            throw new NotImplementedException();
        }
    }
}
