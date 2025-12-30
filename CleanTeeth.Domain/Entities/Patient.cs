using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Entities;

public class Patient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    private Patient () {} // EF core
    public Patient(string name, Email email)
    {
        EnforceNameBusinessRules(name);

        Name = name;
        Email = email ?? throw new BusinessRuleException($"The {nameof(email)} is required");
        Id = Guid.CreateVersion7();
    }

    public void UpdatePatient (string name, string email)
    {
        EnforceNameBusinessRules(name);

        Name = name;
        Email = new Email(email); 
    }

    private void EnforceNameBusinessRules (string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException($"The {nameof(name)} is required");
        }
    }
}
