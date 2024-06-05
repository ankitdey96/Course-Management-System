using CourseManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Domain.Repositories
{
    public interface ICourseRepository:IGenericRepository<Course,Guid>
    {

    }
}
