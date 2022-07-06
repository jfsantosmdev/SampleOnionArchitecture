using FluentValidation;

namespace Application.Features.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("The {PropertyName} field is required.")
                .MaximumLength(80).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("The {PropertyName} field is required}")
                .MaximumLength(80).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

            RuleFor(p => p.BirthDate)
                .NotEmpty().WithMessage("The {PropertyName} field is required}");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("The {PropertyName} field is required}")
                .Matches(@"^\d{4}-\d{4}$").WithMessage("The {PropertyName} must has the format 0000-0000")
                .MaximumLength(9).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("The {PropertyName} field is required}")
               .EmailAddress().WithMessage("The {PropertyName} field must be a valid Email.")
               .MaximumLength(100).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("The {PropertyName} field is required}")
                .MaximumLength(120).WithMessage("The {PropertyName} must be less than {MaxLngth} characters.");

        }
    }
}
