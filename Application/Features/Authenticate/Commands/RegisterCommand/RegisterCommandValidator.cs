using FluentValidation;

namespace Application.Features.Authenticate.Commands.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("The {PropertyName} field is required.")
                .MaximumLength(80).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("The {PropertyName} field is required}")
                .MaximumLength(80).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

            RuleFor(p => p.Email)
              .NotEmpty().WithMessage("The {PropertyName} field is required")
              .EmailAddress().WithMessage("The {PropertyName} field must be a valid Email.")
              .MaximumLength(100).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

            RuleFor(p => p.UserName)
               .NotEmpty().WithMessage("The {PropertyName} field is required}")
               .MaximumLength(80).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("The {PropertyName} field is required}")
                .MaximumLength(16).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("The {PropertyName} field is required}")
                .MaximumLength(16).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.")
                .Equal(p => p.Password).WithMessage("The {PropertyName} must be equal to Password.");
        }
    }
}
