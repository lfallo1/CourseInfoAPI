using System.ComponentModel.DataAnnotations;
using CourseInfoAPI.Models.Abstract;

namespace CourseInfoAPI.Controllers
{
    public class CourseUpdateDto : AbstractCourseDto
    {
        [Required]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}