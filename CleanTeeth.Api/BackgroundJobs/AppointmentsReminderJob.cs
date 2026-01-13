
using CleanTeeth.Application.Appointments.Commands.SendAppointmentReminder;
using MediatR;

namespace CleanTeeth.Api.BackgroundJobs;

public class AppointmentsReminderJob 
{
   private readonly IMediator _mediator;

    public AppointmentsReminderJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RunAsync()
    {
        await _mediator.Send(new SendAppointmentReminderCommand());
    }
}
