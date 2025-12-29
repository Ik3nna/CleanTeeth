using System.ComponentModel.DataAnnotations;

namespace CleanTeeth.Api.Contracts.Patients;

public class CreatePatientRequest
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; }
}
