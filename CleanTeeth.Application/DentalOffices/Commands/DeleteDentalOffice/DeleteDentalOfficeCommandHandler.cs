using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Commands.DeleteDentalOffice;

public class DeleteDentalOfficeCommandHandler : IRequestHandler<DeleteDentalOfficeCommand, Unit>
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDentalOfficeCommandHandler(
        IDentalOfficeRepository dentalOfficeRepository, 
        IUnitOfWork unitOfWork
    )
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteDentalOfficeCommand command, CancellationToken cancellationToken)
    {
        var dentalOffice = await _dentalOfficeRepository.GetByIdAsync(command.Id);

        if (dentalOffice == null)
        {
            throw new NotFoundException($"The dental office with id: {command.Id} was not found");
        }
        
        try
        {
            await _dentalOfficeRepository.DeleteAsync(dentalOffice.Id);
            return Unit.Value;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
