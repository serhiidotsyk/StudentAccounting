﻿using System;

namespace BLL.Models.Course
{
    public class CourseInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
