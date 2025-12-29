using System.ComponentModel.DataAnnotations;

namespace CleanTeeth.Api.Contracts.DentalOffices;

public class UpdateDentalOfficeRequest
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; }
}
