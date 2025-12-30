using MediatR;

namespace CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeQuery;

public class GetDentalOfficeQuery : IRequest<PagedResult<GetDentalOfficesDTO>>
{
    public int page { get; init; } = 1;
    public int pageSize { get; init; } = 10;

    public GetDentalOfficeQuery(int _page = 1, int _pageSize = 10)
    {
        page = _page;
        pageSize = _pageSize;
    }
}
