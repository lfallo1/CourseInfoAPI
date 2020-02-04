using System.ComponentModel.DataAnnotations;
using CourseInfoAPI.Controllers;
using CourseInfoAPI.Models.Abstract;

namespace CourseInfoAPI.ValidationAttributes
{
    public class CourseNameDescriptionDifferent : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            AbstractCourseDto course = (AbstractCourseDto)validationContext.ObjectInstance;
            if (course.Title == course.Description)
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(AbstractCourseDto) });
            }
            return ValidationResult.Success;
        }
    }
}
