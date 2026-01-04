using MediatR;

namespace CleanTeeth.Application.Appointments.Queries.GetAppointmentDetail;

public class GetAppointmentDetailQuery : IRequest<AppointmentDetailDTO>
{
    public Guid Id { get; set; }
}
