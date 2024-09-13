using CourseManagement.Domain.Repositories;
using CourseManagement.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public ICourseRepository CourseRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public ICourseTopicRepository CourseTopicRepository { get; private set; }

        public ICourseEnrollmentRepository CourseEnrollmentRepository { get; private set; }
        public IDapperUtility DapperUtility { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            CourseRepository = new CourseRepository(dbContext);
            UserRepository = new UserRepository(dbContext);
            CourseTopicRepository = new CourseTopicRepository(dbContext);
            CourseEnrollmentRepository = new CourseEnrollmentRepository(dbContext);
            DapperUtility = new DapperUtility(dbContext.Database.GetDbConnection());
        }


        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public async Task SaveAsync()
        {
          await _dbContext.SaveChangesAsync();
        }
    }
}
