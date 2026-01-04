using FluentValidation.Results;

namespace CleanTeeth.Application.Common.Exceptions;

public class CustomValidationException : Exception
{
    public List<string> ValidationErrors { get; set; } = new List<string>();
    public CustomValidationException(string errorMessage)
    {
        ValidationErrors.Add(errorMessage);
    }
    public CustomValidationException(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }
}
