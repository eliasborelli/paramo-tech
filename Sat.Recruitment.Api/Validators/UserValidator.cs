using FluentValidation;
using Sat.Recruitment.Api.Dtos.Request;
using Sat.Recruitment.Api.Validators.Custom;

namespace Sat.Recruitment.Api.Validators
{
    public class UserValidator : AbstractValidator<UserRequestDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("The name is required");

            RuleFor(x => x.Address)
            .NotEmpty()
            .NotNull()
            .WithMessage("The address is required");

            RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("The email is required")
            .SetValidator(new EmailPropertyValidator<UserRequestDTO, string>())
            .WithMessage("The email is not valid");

            RuleFor(x => x.Phone)
            .NotEmpty()
            .NotNull()
            .WithMessage("The phone is required");
        }

    }
}
