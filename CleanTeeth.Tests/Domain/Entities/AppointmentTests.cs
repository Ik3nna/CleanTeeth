using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.Entities
{
    [TestClass]
    public class AppointmentTests
    {
        private Guid _patientId = Guid.NewGuid();
        private Guid _dentistId = Guid.NewGuid();
        private Guid _dentalOfficeId = Guid.NewGuid();
        private TimeInterval _interval = new TimeInterval(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

        [TestMethod]
        /// <summary>
        /// Test to ensure that the Appointment constructor initializes properties correctly
        /// and sets the status to Scheduled.
        /// </summary> 
        public void Constructor_ValidAppointment_StatusIsScheduled()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _interval);

            Assert.AreEqual(_patientId, appointment.PatientId); // Ensure PatientId is set correctly
            Assert.AreEqual(_dentistId, appointment.DentistId); // Ensure DentistId is set correctly
            Assert.AreEqual(_dentalOfficeId, appointment.DentalOfficeId); // Ensure DentalOfficeId is set correctly
            Assert.AreEqual(_interval, appointment.TimeInterval); // Ensure TimeInterval is set correctly
            Assert.AreEqual(AppointmentStatus.Scheduled, appointment.Status); // Ensure status is set to Scheduled
            Assert.AreNotEqual(Guid.Empty, appointment.Id); // Ensure Appointment Id is set
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        /// <summary>
        /// Test to ensure that the Appointment constructor throws an exception
        /// when provided with a start time in the past.
        /// </summary>
        public void Constructor_StartTimeInThePast_Throws()
        {
            var interval = new TimeInterval(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow); // Start time in the past
            new Appointment(_patientId, _dentistId, _dentalOfficeId, interval); // Should throw exception
        }

        [TestMethod]
        /// <summary>
        /// Test to ensure that cancelling an appointment changes its status to Cancelled.
        /// </summary>
        public void Cancel_CancellingAppointment_ChangesStatusToCancelled()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _interval); // Create a new appointment
            appointment.Cancel(); // Cancel the appointment
            Assert.AreEqual(AppointmentStatus.Cancelled, appointment.Status); // Verify status is Cancelled
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        /// <summary>
        /// Test to ensure that cancelling an appointment that is not in Scheduled status
        /// throws a BusinessRuleException.
        /// </summary>
        public void Cancel_CancellingAppointment_ThrowsIfStatusIsNotScheduled()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _interval); // Create a new appointment
            appointment.Cancel(); // First cancel the appointment
            appointment.Cancel(); // Attempt to cancel again, should throw exception
        }

        [TestMethod]
        /// <summary>
        /// Test to ensure that completing an appointment changes its status to Completed.
        /// </summary>
        public void Complete_CompletingAppointment_ChangesStatusToCompleted()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _interval); // Create a new appointment
            appointment.Complete(); // Complete the appointment
            Assert.AreEqual(AppointmentStatus.Completed, appointment.Status); // Verify status is Completed
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        /// <summary>
        /// Test to ensure that completing an appointment that is not in Scheduled status
        /// throws a BusinessRuleException.
        /// </summary>
        public void Complete_CompletingAppointment_ThrowsIfStatutsIsNotScheduled()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _interval); // Create a new appointment
            appointment.Cancel(); // First cancel the appointment
            appointment.Complete(); // Attempt to complete the appointment
        }
    }
}