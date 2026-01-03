using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Dentists.Commands.DeleteDentist;

public class DeleteDentistCommandHandler : IRequestHandler<DeleteDentistCommand, Unit>
{
    private readonly IDentistRepository _dentistRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteDentistCommandHandler(
        IDentistRepository dentistRepository, 
        IUnitOfWork unitOfWork
    )
    {
        _dentistRepository = dentistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteDentistCommand command, CancellationToken cancellationToken)
    {
        var dentist = await _dentistRepository.GetByIdAsync(command.Id);

        if (dentist == null)
        {
            throw new NotFoundException($"The dentist with id: {command.Id} was not found");
        }
        
        try
        {
            await _dentistRepository.DeleteAsync(dentist.Id);
            return Unit.Value;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
