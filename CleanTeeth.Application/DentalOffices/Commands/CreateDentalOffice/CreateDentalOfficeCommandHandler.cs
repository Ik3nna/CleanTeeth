using AutoMapper;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;

public class CreateDentalOfficeCommandHandler : IRequestHandler<CreateDentalOfficeCommand, DentalOfficeDTO> 
// IRequestHandler interface is from MediatR library and is used to handle requests of
// type CreateDentalOfficeCommand and return a response of type DentalOfficeDTO.
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDentalOfficeCommandHandler(
        IDentalOfficeRepository dentalOfficeRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DentalOfficeDTO> Handle(CreateDentalOfficeCommand command, CancellationToken cancellationToken)
    {
        var dentalOffice = new DentalOffice(command.Name);
        try
        {
            await _dentalOfficeRepository.AddAsync(dentalOffice);
            var dto = _mapper.Map<DentalOfficeDTO>(dentalOffice);
            return dto;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        
    }
}
