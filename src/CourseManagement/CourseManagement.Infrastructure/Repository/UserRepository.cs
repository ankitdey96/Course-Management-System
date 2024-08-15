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
    public class UserRepository:GenericRepository<User,Guid>,IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
