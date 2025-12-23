using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Tests.Domain.Entities
{
    [TestClass]
    public class DentalOfficeTests
    {
        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        /// <summary>
        /// Test to ensure that the DentalOffice constructor throws an exception
        /// when provided with a null name.
        /// </summary>
        public void Constructor_NullName_Throws()
        {
            new DentalOffice(null!);
        }

    }
}