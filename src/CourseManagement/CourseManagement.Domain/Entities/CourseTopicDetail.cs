using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Domain.Entities
{
    public class CourseTopicDetail:IEntity<Guid>
    {
        public Guid Id {  get; set; }

        public Guid TopicId { get; set; }

        public string TopicName { get; set; }

        public CourseTopic CourseTopic { get; set; }
    }
}
