using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseInfoAPI.Controllers;
using CourseInfoAPI.ValidationAttributes;

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
        [AuthorMinAgeAttribute(MinAge = 13)]
        public DateTimeOffset DateOfBirth { get; set; }

        [Required]
        [MaxLength(50)]
        public string MainCategory { get; set; }

        public string ZipCode { get; set; }

        public string CountryCode { get; set; }

        public ICollection<CourseCreateDto> Courses { get; set; } = new List<CourseCreateDto>();
    }
}
