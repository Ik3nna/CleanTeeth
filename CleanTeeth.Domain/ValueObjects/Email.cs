using System.Text.RegularExpressions;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.ValueObjects;

public record Email
{
    public string Value { get; } = null!;
    private Email () {}
    public Email(string email)
    {
        EnforceBusinessRules(email);

        Value = email;
    }

    private static bool IsValid(string email)
        => Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase);
    
    private void EnforceBusinessRules (string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BusinessRuleException("Email is required");
        }

        if (!IsValid(email))
        {
            throw new BusinessRuleException("Invalid email format");
        }
    } 
}
