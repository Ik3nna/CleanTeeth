using FluentValidation;

namespace CleanTeeth.Application.Dentists.Commands.CreateDentist;

public class CreateDentistCommandValidator : AbstractValidator<CreateDentistCommand>
{
    public CreateDentistCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Dentist's name is required.")
            .MaximumLength(150).WithMessage("Dentist's name must not exceed 150 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Dentist's Email is required.")
            .MaximumLength(256).WithMessage("Dentist's Email must not exceed 256 characters.")
            .EmailAddress().WithMessage("Invalid email address format.");
    }
}
