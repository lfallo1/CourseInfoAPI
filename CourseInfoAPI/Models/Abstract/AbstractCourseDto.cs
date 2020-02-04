using System.ComponentModel.DataAnnotations;
using CourseInfoAPI.ValidationAttributes;

namespace CourseInfoAPI.Models.Abstract
{
    [CourseNameDescriptionDifferent(ErrorMessage = "Description should be different from title")]
    public abstract class AbstractCourseDto //: IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public virtual string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult("Description should be different from title",
        //            new[] { validationContext.ObjectType.Name });
        //    }
        //}
    }
}
