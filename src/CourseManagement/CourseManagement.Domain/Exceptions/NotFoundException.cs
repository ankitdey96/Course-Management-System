﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Domain.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string ModelName):base(ModelName + "Not Found")
        {

        }
    }
}
