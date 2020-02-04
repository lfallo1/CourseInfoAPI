using CourseInfoAPI.ValidationAttributes.FluentValidatorsCustom;
using FluentValidation;

namespace CourseInfoAPI.ValidationAttributes.FluentValidatorExtensions
{
    public static class AuthorValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidZipCode<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new ValidZipcodeForCountry());
        }
    }
}
