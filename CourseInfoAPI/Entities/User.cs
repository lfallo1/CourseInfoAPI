using System;
using System.ComponentModel.DataAnnotations;

namespace CourseInfoAPI.Entities
{
    public class User
    {
        [Key]
        [Required]
        [MinLength(8)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
    }
}
