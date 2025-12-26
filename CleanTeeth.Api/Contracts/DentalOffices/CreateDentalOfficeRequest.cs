using System.ComponentModel.DataAnnotations;

namespace CleanTeeth.Api.Contracts.DentalOffices;

public class CreateDentalOfficeRequest
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; }
}
