using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Domain.Entities
{
    public class CourseTopic:IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid CourseID {  get; set; }
        public string TopicName {  get; set; }
        public Course Course { get; set; }
        public List<CourseTopicDetail> TopicDetails { get; set; }
    }
}
