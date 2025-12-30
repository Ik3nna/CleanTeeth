using MediatR;

namespace CleanTeeth.Application.Patients.Queries.GetPatientDetail;

public class GetPatientDetailQuery : IRequest<PatientDetailDTO>
{
    public required Guid Id { get; set; }
}
