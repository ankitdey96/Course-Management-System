using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Domain.Repositories
{
    public interface IUnitOfWork:IAsyncDisposable,IDisposable
    {
        public ICourseRepository CourseRepository { get; }
        Task SaveAsync();
    }
}
