using FluentValidation;

namespace CleanTeeth.Application.Dentists.Commands.UpdateDentist;

public class UpdateDentistCommandValidator : AbstractValidator<UpdateDentistCommand>
{
     public UpdateDentistCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Dental office name is required.")
            .MaximumLength(150).WithMessage("Dental office name must not exceed 150 characters.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Dentist's Email is required.")
            .MaximumLength(256).WithMessage("Dentist's Email must not exceed 256 characters.")
            .EmailAddress().WithMessage("Invalid email address format.");
    }
}

