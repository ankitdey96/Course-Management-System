using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Services
{
    public class UserService :IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUser(string Username, string PasswordHash, string FirstName, string LastName, string Email, Guid Id)
        {
            User oUser =new User
            {
                FullName = FirstName+ " " + LastName,
                Email = Email,
                Id = Id,
                PasswordHash = PasswordHash,
                FirstName = FirstName,
                LastName = LastName
            };

            await _unitOfWork.UserRepository.AddAsync(oUser);
            await _unitOfWork.SaveAsync();
        }
    }
}
