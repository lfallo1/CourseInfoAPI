using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseInfoAPI.Controllers;

namespace CourseInfoAPI.Models
{
    public class AuthorCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        [Required]
        [MaxLength(50)]
        public string MainCategory { get; set; }

        public ICollection<CourseCreateDto> Courses { get; set; } = new List<CourseCreateDto>();
    }
}
