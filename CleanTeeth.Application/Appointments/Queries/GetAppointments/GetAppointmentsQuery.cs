using MediatR;

namespace CleanTeeth.Application.Appointments.Queries.GetAppointments;

public class GetAppointmentsQuery : IRequest<PagedResult<AppointmentListDTO>>
{
    public int page { get; set; }
    public int pageSize { get; set; }
    public Guid? patientId { get; set; }
    public Guid? dentistId { get; set; }
    public Guid? dentalOfficeId { get; set; }
    public DateTime? startDate { get; set; }
    public DateTime? endDate { get; set; }
    public GetAppointmentsQuery(
        int _page = 1, 
        int _pageSize = 10, 
        Guid? _patientId = null, 
        Guid? _dentistId = null,
        Guid? _dentalOfficeId = null,
        DateTime? _startDate = null,
        DateTime? _endDate = null
    )
    {
        page = _page;
        pageSize = _pageSize;
        patientId = _patientId;
        dentistId = _dentistId;
        dentalOfficeId = _dentalOfficeId;
        startDate = _startDate;
        endDate = _endDate;
    }
}
