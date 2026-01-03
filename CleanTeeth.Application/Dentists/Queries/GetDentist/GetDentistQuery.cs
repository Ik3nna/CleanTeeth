using CleanTeeth.Application.Dentists.Queries.GetDentist;
using MediatR;

namespace CleanTeeth.Application.Dentists.Queries.GetDentist;

public class GetDentistQuery : IRequest<PagedResult<GetDentistDTO>>
{
    public int page { get; set; }
    public int pageSize { get; set; }
    public string? name { get; }
    public string? email { get; }
    public GetDentistQuery(int _page = 1, int _pageSize = 10, string? _name = null, string? _email = null)
    {
        page = _page;
        pageSize = _pageSize;
        name = _name;
        email = _email;
    }
}
