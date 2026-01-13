using AutoMapper;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Appointments.Queries.GetAppointments;

public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, PagedResult<AppointmentListDTO>>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;

    public GetAppointmentsQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<AppointmentListDTO>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetFilteredAsync(
            request.page, 
            request.pageSize, 
            request.patientId, 
            request.dentistId,
            request.dentalOfficeId,
            request.startDate,
            request.endDate,
            request.appointmentStatus
        );
        // Map only the Items
        var dtoItems = _mapper.Map<List<AppointmentListDTO>>(appointment.Items);

        var dto = new PagedResult<AppointmentListDTO>
        {
            Items = dtoItems,
            Page = appointment.Page,
            PageSize = appointment.PageSize,
            TotalCount = appointment.TotalCount
        };
        return dto;
    }
}