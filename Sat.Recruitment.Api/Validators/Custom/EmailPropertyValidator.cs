using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Api.Validators.Custom
{
    public class EmailPropertyValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public override string Name => "EmailPropertyValidator";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            return Regex.IsMatch(value.ToString(), GetRegexEmail());
        }

        private string GetRegexEmail()
        {
            //Minimum eight characters, at least one letter, one number and one special character:
            return @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                      + "@"
                      + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
        }
    }
}
