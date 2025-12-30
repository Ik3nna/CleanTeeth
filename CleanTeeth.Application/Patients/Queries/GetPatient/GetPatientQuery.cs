using MediatR;

namespace CleanTeeth.Application.Patients.Queries.GetPatient;

public class GetPatientQuery : IRequest<PagedResult<GetPatientDTO>>
{
    public int page { get; set; }
    public int pageSize { get; set; }
    public GetPatientQuery(int _page = 1, int _pageSize = 10)
    {
        page = _page;
        pageSize = _pageSize;
    }
}
