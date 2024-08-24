using CourseManagement.Application.Interfaces;
using CourseManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Services
{
    public class CourseOutLineManagementService:ICourseOutLineManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseOutLineManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }
}
