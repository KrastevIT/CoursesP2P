﻿using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models
{
    public class Lecture
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Name { get; set; }

        public string Video { get; set; }

        public string Asset { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
