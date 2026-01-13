using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Application.Notifications;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Domain.ValueObjects;
using MediatR;

namespace CleanTeeth.Application.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDTO>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INotifications _notifications;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper,
        INotifications notifications
    )
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notifications = notifications;
    }

    public async Task<AppointmentDTO> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var existOverlap = await _appointmentRepository.OverlapExists(command.DentistId, command.StartDate, command.EndDate);

        if (existOverlap)
        {
            throw new CustomValidationException("The dentist has an appointment that overlaps with this time slot");
        }

        var timeInterval = new TimeInterval(command.StartDate, command.EndDate);
        var appointment = new Appointment(command.PatientId, command.DentistId, command.DentalOfficeId, timeInterval);

        try
        {
            await _appointmentRepository.AddAsync(appointment);

            // Reload appointment with navigation properties
            var appointmentWithNav = await _appointmentRepository.GetByIdAsync(appointment.Id);

            // Notification setup
            var notificationDTO = _mapper.Map<AppointmentConfirmationDTO>(appointmentWithNav);
            await _notifications.SendAppointmentConfirmation(notificationDTO);

            // Map to DTO. This is what is returned on creation of a new appointment
            var dto = _mapper.Map<AppointmentDTO>(appointmentWithNav);
            return dto;
        } catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
