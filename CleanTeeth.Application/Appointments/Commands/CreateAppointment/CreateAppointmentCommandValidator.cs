using FluentValidation;

namespace CleanTeeth.Application.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x=>x.EndDate)
            .GreaterThan(x=>x.StartDate)
            .WithMessage("Start date must be before end date");
    }
}
