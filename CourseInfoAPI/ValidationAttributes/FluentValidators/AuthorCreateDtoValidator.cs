using CourseInfoAPI.Models;
using CourseInfoAPI.ValidationAttributes.FluentValidatorExtensions;
using FluentValidation;

namespace CourseInfoAPI.ValidationAttributes.FluentValidators
{
    public class AuthorCreateDtoValidator : AbstractValidator<AuthorCreateDto>
    {
        public AuthorCreateDtoValidator()
        {
            RuleFor(a => a.FirstName).NotEqual("Dinosaur").WithMessage("You're not a dinosaur. Pick another name");
            RuleFor(a => a.LastName).NotEqual("Dinosaur").WithMessage("Just stop");
            RuleFor(a => a.ZipCode).ValidZipCode();
        }
    }
}
