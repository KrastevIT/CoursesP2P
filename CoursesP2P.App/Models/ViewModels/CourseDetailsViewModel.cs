﻿using CoursesP2P.Models.Enum;

namespace CoursesP2P.App.Models.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Image { get; set; }

        public string InstructorFullName { get; set; }
    }
}