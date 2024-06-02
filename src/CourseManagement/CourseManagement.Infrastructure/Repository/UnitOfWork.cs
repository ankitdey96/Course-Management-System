using CourseManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
       

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
