﻿using System;
namespace CourseInfoAPI.Models
{
    public class UserDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
    }
}
