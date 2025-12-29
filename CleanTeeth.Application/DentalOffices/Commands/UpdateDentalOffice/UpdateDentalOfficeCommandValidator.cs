using FluentValidation;

namespace CleanTeeth.Application.DentalOffices.Commands.UpdateDentalOffice;

public class UpdateDentalOfficeCommandValidator : AbstractValidator<UpdateDentalOfficeCommand>
{
     public UpdateDentalOfficeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Dental office name is required.")
            .MaximumLength(50).WithMessage("Dental office name must not exceed 50 characters.");
    }
}
