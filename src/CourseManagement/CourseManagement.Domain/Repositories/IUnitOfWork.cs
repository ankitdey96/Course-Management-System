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
        public IUserRepository UserRepository { get; }
        public ICourseTopicRepository CourseTopicRepository { get; }
        public ICourseEnrollmentRepository CourseEnrollmentRepository { get; }
        Task SaveAsync();

        ValueTask DisposeAsync();
    }
}
