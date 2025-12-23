using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.Entities
{
    [TestClass]
    public class PatientTests
    {
        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        /// <summary>
        /// Test to ensure that the Patient constructor throws an exception
        /// when provided with a null name.
        /// </summary>
        public void Constructor_NullName_Throws()
        {
            var email = new Email("felipe@example.com");
            new Patient(null!, email);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        /// <summary>   
        /// Test to ensure that the Patient constructor throws an exception
        /// when provided with a null email.
        /// </summary>
        public void Constructor_NullEmail_Throws()
        {
            new Patient("Felipe", email: null!);
        }

        [TestMethod]
        /// <summary>
        /// Test to ensure that the Patient constructor initializes properties correctly
        /// when provided with valid parameters.
        /// </summary>
        public void Constructor_ValidPatient_NoExceptions()
        {
            var email = new Email("felipe@example.com");
            new Patient("Felipe", email);
        }
    }
}