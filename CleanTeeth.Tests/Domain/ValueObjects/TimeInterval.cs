using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.ValueObjects;

[TestClass] // Marks this class as a test class
public class TimeIntervalTests
{
    [TestMethod] // Marks this method as a test method
    [ExpectedException(typeof(BusinessRuleException))] // The expected exception type that should be thrown
    /// <summary>
    /// Tests that the constructor throws a BusinessRuleException when the start time is after the end time.
    /// </summary>
    public void Constructor_StartIsAfterEnd_Throws()
    {
        new TimeInterval(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1)); // Act: Attempt to create a TimeInterval with start time after end time
    }

    [TestMethod] // Marks this method as a test method
    /// <summary>
    /// Tests that the constructor successfully creates a TimeInterval object when valid start and end times are provided.
    /// </summary>
    public void Constructor_ValidTimeInterval_NoExceptions()
    {
        new TimeInterval(DateTime.UtcNow, DateTime.UtcNow.AddHours(1)); // Act: Attempt to create a valid TimeInterval
    }
}
