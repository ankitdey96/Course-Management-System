﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Domain.Entities
{
    public class Course : IEntity<Guid>
    {
        public Guid Id { get; set; }

    }
}
