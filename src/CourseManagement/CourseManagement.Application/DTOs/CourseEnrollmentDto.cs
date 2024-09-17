﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.DTOs
{
    public class CourseEnrollmentDto
    {
        public string CourseName {  get; set; }
        public string StudentName {  get; set; }

        public DateTime EnrollmentDate {  get; set; }
    }
}