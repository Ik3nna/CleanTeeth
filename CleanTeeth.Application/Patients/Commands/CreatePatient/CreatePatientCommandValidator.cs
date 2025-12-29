using System;
using FluentValidation;

namespace CleanTeeth.Application.Patients.Commands.CreatePatient;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Patient's name is required.")
            .MaximumLength(150).WithMessage("Patient's name must not exceed 150 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Patient's Email is required.")
            .MaximumLength(256).WithMessage("Patient's Email must not exceed 256 characters.")
            .EmailAddress().WithMessage("Invalid email address format.");
    }
}
