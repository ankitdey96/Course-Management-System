using CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Infrastructure.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionstring;
        public ApplicationDbContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public DbSet<Course>Course { get; set; }
    }
}
