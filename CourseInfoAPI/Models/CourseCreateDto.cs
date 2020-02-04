using System;
using System.ComponentModel.DataAnnotations;

namespace CourseInfoAPI.Controllers
{
    public class CourseCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
    }
}