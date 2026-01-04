
using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Appointments.Queries.GetAppointmentDetail;

public class GetAppointmentDetailQueryHandler : IRequestHandler<GetAppointmentDetailQuery, AppointmentDetailDTO>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;

    public GetAppointmentDetailQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }
    public async Task<AppointmentDetailDTO> Handle(GetAppointmentDetailQuery request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Appointment not found");
        var dto = _mapper.Map<AppointmentDetailDTO>(appointment);
        return dto;
    }
}
