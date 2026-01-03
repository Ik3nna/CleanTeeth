using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Dentists.Queries.GetDentistDetail;

public class GetDentistDetailQueryHandler : IRequestHandler<GetDentistDetailQuery, DentistDetailDTO>
{
    private readonly IDentistRepository _dentistRepository;
    private readonly IMapper _mapper;

    public GetDentistDetailQueryHandler(IDentistRepository dentistRepository, IMapper mapper)
    {
        _dentistRepository = dentistRepository;
        _mapper = mapper;
    }

    public async Task<DentistDetailDTO> Handle(GetDentistDetailQuery request, CancellationToken cancellationToken)
    {
        var dentist = await _dentistRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Dentist not found");
        var dto = _mapper.Map<DentistDetailDTO>(dentist);
        return dto;
    }
}
