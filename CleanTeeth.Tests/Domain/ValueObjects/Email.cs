using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.ValueObjects;
[TestClass] // Marks this class as a test class
public class EmailTests
{
    [TestMethod] // Marks this method as a test method
    [ExpectedException(typeof(BusinessRuleException))] // The expected exception type that should be thrown
    /// <summary>
    /// Tests that the constructor throws a BusinessRuleException when a null email is provided.
    /// </summary>
    public void Constructor_NullEmail_Throws()
    {
        new Email(null!); // Act: Attempt to create an Email with a null value
    }

    [TestMethod] // Marks this method as a test method
    [ExpectedException(typeof(BusinessRuleException))] // The expected exception type that should be thrown
    /// <summary>
    /// Tests that the constructor throws a BusinessRuleException when an email without an "@" symbol is provided.
    /// </summary>
    public void Constructor_EmailWithoutAtSymbol_Throws()
    {
        new Email("test.com"); // Act: Attempt to create an Email without an "@" symbol
    }

    [TestMethod] // Marks this method as a test method
    /// <summary>
    /// Tests that the constructor successfully creates an Email object when a valid email is provided.
    /// </summary>
    public void Constructor_ValidEmail_Succeeds()
    {
        new Email("test@example.com"); // Act: Attempt to create an Email with a valid value
    }
}
