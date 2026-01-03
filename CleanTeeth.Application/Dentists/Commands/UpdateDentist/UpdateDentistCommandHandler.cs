using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Application.Dentists.Commands.CreateDentist;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Dentists.Commands.UpdateDentist;

public class UpdateDentistCommandHandler : IRequestHandler<UpdateDentistCommand, DentistDTO>
{
    private readonly IDentistRepository _dentistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDentistCommandHandler(
        IDentistRepository dentistRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _dentistRepository = dentistRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DentistDTO> Handle(UpdateDentistCommand command, CancellationToken cancellationToken)
    {
        var dentist = await _dentistRepository.GetByIdAsync(command.Id);

        if (dentist == null)
        {
            throw new NotFoundException($"The dental office with id: {command.Id} was not founf");
        }
        
        dentist.UpdateDentist(command.Name, command.Email);

        try
        {
            await _dentistRepository.UpdateAsync(dentist);
            var dto = _mapper.Map<DentistDTO>(dentist);
            return dto;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        
    }
}
