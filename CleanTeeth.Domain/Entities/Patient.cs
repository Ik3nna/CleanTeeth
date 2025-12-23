using System;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.Entities;

public class Patient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    public Patient(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException($"The {nameof(name)} is required");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BusinessRuleException($"The {nameof(email)} is required");
        }

        if (!email.Contains("@"))
        {
            throw new BusinessRuleException($"The {nameof(email)} is invalid");
        }

        Name = name;
        Email = email;
        Id = Guid.CreateVersion7();
    }
}
