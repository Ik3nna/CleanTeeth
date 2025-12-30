using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Commands.UpdateDentalOffice;

public class UpdateDentalOfficeCommandHandler : IRequestHandler<UpdateDentalOfficeCommand, DentalOfficeDTO>
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDentalOfficeCommandHandler(
        IDentalOfficeRepository dentalOfficeRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DentalOfficeDTO> Handle(UpdateDentalOfficeCommand command, CancellationToken cancellationToken)
    {
        var dentalOffice = await _dentalOfficeRepository.GetByIdAsync(command.Id);

        if (dentalOffice == null)
        {
            throw new NotFoundException($"The dental office with id: {command.Id} was not founf");
        }
        
        dentalOffice.UpdateName(command.Name);

        try
        {
            await _dentalOfficeRepository.UpdateAsync(dentalOffice);
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
