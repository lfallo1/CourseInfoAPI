using System;
using System.ComponentModel.DataAnnotations;
using CourseInfoAPI.Helpers;

namespace CourseInfoAPI.ValidationAttributes
{
    public class AuthorMinAgeAttribute : ValidationAttribute
    {

        public int MinAge { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            MinAge = MinAge == 0 ? 18 : MinAge;

            var dateOfBirth = (DateTimeOffset)value;

            if (dateOfBirth.GetCurrentAge() < MinAge)
            {
                return new ValidationResult($"Author must be at least {MinAge} years old",
                    new[] { validationContext.ObjectType.Name });
            }
            return ValidationResult.Success;
        }
    }
}
