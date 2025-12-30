using System;
using FluentValidation;

namespace CleanTeeth.Application.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
     public UpdatePatientCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Dental office name is required.")
            .MaximumLength(150).WithMessage("Dental office name must not exceed 150 characters.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Patient's Email is required.")
            .MaximumLength(256).WithMessage("Patient's Email must not exceed 256 characters.")
            .EmailAddress().WithMessage("Invalid email address format.");
    }
}

