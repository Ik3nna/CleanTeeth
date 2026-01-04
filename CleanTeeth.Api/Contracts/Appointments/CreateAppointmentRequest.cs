using System;
using System.ComponentModel.DataAnnotations;

namespace CleanTeeth.Api.Contracts.Appointments
{
    public class CreateAppointmentRequest
    {
        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid DentistId { get; set; }

        [Required]
        public Guid DentalOfficeId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        [CustomValidation(typeof(CreateAppointmentRequest), nameof(ValidateEndDate))]
        public DateTime EndDate { get; set; }

        // Optional: Custom validation to ensure EndDate is after StartDate
        public static ValidationResult? ValidateEndDate(DateTime endDate, ValidationContext context)
        {
            var instance = (CreateAppointmentRequest)context.ObjectInstance;
            if (endDate <= instance.StartDate)
            {
                return new ValidationResult("End date must be after start date.", new[] { nameof(EndDate) });
            }

            return ValidationResult.Success;
        }
    }
}
