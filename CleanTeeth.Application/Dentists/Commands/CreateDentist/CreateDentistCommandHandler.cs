using AutoMapper;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Domain.ValueObjects;
using MediatR;

namespace CleanTeeth.Application.Dentists.Commands.CreateDentist;

public class CreateDentistCommandHandler : IRequestHandler<CreateDentistCommand, DentistDTO>
{
    private readonly IDentistRepository _dentistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDentistCommandHandler(
        IDentistRepository dentistRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _dentistRepository = dentistRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DentistDTO> Handle(CreateDentistCommand command, CancellationToken cancellationToken)
    {
        var dentist = new Dentist(command.Name, new Email(command.Email));
        try
        {
            await _dentistRepository.AddAsync(dentist);
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
