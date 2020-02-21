﻿using CoursesP2P.ViewModels.Lectures.ViewModels;
using System.Collections.Generic;

namespace CoursesP2P.ViewModels.Courses.ViewModels
{
    public class CourseEnrolledViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public CategoryViewModel Category { get; set; }

        public string Image { get; set; }

        public ICollection<CourseLecturesViewModel> Lectures { get; set; }

        public string InstructorFullName { get; set; }
    }
}