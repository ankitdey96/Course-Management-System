using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Repositories;
using CourseManagement.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Infrastructure.Repository
{
    public class CourseTopicRepository : GenericRepository<CourseTopic, Guid>, ICourseTopicRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseTopicRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            _context = dbContext;
        }
    }
}
