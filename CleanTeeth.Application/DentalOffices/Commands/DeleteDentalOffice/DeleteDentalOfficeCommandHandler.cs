using System;
using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Commands.DeleteDentalOffice;

public class DeleteDentalOfficeCommandHandler : IRequestHandler<DeleteDentalOfficeCommand, Unit>
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteDentalOfficeCommandHandler(
        IDentalOfficeRepository dentalOfficeRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteDentalOfficeCommand command, CancellationToken cancellationToken)
    {
        var dentalOffice = await _dentalOfficeRepository.GetDentalOfficeByIdAsync(command.Id);

        if (dentalOffice == null)
        {
            throw new NotFoundException($"The dental office with id: {command.Id} was not founf");
        }
        
        try
        {
            await _dentalOfficeRepository.DeleteDentalOfficeAsync(dentalOffice.Id);
            return Unit.Value;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        
    }
}
