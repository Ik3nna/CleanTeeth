using MediatR;

namespace CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;

public class CreateDentalOfficeCommand : IRequest<DentalOfficeDTO> 
// The IRequest interface is from MediatR library and is used to define a request that expects a response of type DentalOfficeDTO.
{
    public required string Name { get; set; }
}
