using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseInfoAPI.Controllers
{
    public class CourseCreateDto : IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description)
            {
                yield return new ValidationResult("Description should be different from title",
                    new[] { validationContext.ObjectType.Name });
            }
        }
    }
}