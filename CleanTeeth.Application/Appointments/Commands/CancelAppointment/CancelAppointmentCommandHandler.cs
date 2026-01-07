using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Appointments.Commands.CancelAppointment;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Unit>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CancelAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CancelAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(command.Id) ?? throw new NotFoundException($"Appointment with {command.Id} not found");

        appointment.Cancel();

        try
        {
            await _appointmentRepository.UpdateAsync(appointment);
            return Unit.Value;
        } catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
