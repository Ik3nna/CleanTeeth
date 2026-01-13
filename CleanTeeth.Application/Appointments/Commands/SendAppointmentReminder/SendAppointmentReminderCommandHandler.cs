using AutoMapper;
using CleanTeeth.Application.Notifications;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Appointments.Commands.SendAppointmentReminder;

public class SendAppointmentReminderCommandHandler : IRequestHandler<SendAppointmentReminderCommand, Unit>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly INotifications _notifications;
    private readonly IMapper _mapper;
    public SendAppointmentReminderCommandHandler(IAppointmentRepository appointmentRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper,
        INotifications notifications
    )
    {
        _appointmentRepository = appointmentRepository;
        _notifications = notifications;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(SendAppointmentReminderCommand command, CancellationToken cancellationToken)
    {
        var startDate = DateTime.UtcNow.Date.AddDays(1);
        var endDate = startDate.AddDays(1);

        var appointments = await _appointmentRepository.GetAppointmentsForReminderAsync(
           startDate,
           endDate,
           AppointmentStatus.Scheduled
        );

        foreach (var appointment in appointments)
        {
            var appointmentDTO = _mapper.Map<AppointmentReminderDTO>(appointment);
            await _notifications.SendAppointmentReminder(appointmentDTO);
        }

        return Unit.Value;
    }
}
