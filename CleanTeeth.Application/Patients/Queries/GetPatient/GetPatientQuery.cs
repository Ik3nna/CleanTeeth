using MediatR;

namespace CleanTeeth.Application.Patients.Queries.GetPatient;

public class GetPatientQuery : IRequest<PagedResult<GetPatientDTO>>
{
    public int page { get; set; }
    public int pageSize { get; set; }
    public string? name { get; }
    public string? email { get; }
    public GetPatientQuery(int _page = 1, int _pageSize = 10, string? _name = null, string? _email = null)
    {
        page = _page;
        pageSize = _pageSize;
        name = _name;
        email = _email;
    }
}
