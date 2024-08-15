using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(string Username, string PasswordHash,string FirstName,string LastName,string Email,Guid Id);
    }
}
