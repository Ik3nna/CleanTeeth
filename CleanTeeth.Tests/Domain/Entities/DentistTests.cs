using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.Entities
{
    [TestClass]
    public class DentistTests
    {
        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        /// <summary>
        /// Test to ensure that the Dentist constructor throws an exception
        /// when provided with a null name.
        /// </summary>
        public void Constructor_NullName_Throws()
        {
            var email = new Email("felipe@example.com");
            new Dentist(null!, email);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        /// <summary>   
        /// Test to ensure that the Dentist constructor throws an exception
        /// when provided with a null email.
        /// </summary>
        public void Constructor_NullEmail_Throws()
        {
            new Dentist("Felipe", email: null!);
        }

        [TestMethod]
        /// <summary>
        /// Test to ensure that the Dentist constructor initializes properties correctly
        /// when provided with valid parameters.
        /// </summary>
        public void Constructor_ValidDentist_NoExceptions()
        {
            var email = new Email("felipe@example.com");
            new Dentist("Felipe", email);
        }

    }
}