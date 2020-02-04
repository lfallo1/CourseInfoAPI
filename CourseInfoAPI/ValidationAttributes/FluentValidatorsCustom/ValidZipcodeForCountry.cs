using System;
using System.Linq;
using CourseInfoAPI.Models;
using FluentValidation.Validators;

namespace CourseInfoAPI.ValidationAttributes.FluentValidatorsCustom
{
    public class ValidZipcodeForCountry : PropertyValidator
    {
        public ValidZipcodeForCountry() : base("{PropertyValue} is not a valid zip code in {CountryCode}")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var zipCode = (string)context.PropertyValue;
            var contextData = (AuthorCreateDto)context.ParentContext.InstanceToValidate;
            var countryCode = contextData.CountryCode;

            if (string.IsNullOrEmpty(zipCode) || string.IsNullOrEmpty(countryCode))
            {
                return true;
            }

            context.MessageFormatter.AppendArgument("CountryCode", countryCode);

            return IsValidZipCodeByCountry(contextData.ZipCode, countryCode);
        }

        private bool IsValidZipCodeByCountry(string zipCode, string countryCode)
        {
            if (countryCode.ToLower().Equals("us"))
            {
                //only allow digits for US
                return zipCode.All(c => char.IsDigit(c) || c == '-');
            }

            //else allow letters or digits
            return zipCode.All(c => {
                return char.IsLetterOrDigit(c) || c == '-';
            });
        }
    }
}
