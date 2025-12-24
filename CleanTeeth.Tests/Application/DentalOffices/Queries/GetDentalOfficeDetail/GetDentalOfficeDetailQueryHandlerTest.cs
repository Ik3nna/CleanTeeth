using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Company.TestProject1;

[TestClass]
public class GetDentalOfficeDetailQueryHandlerTest
{
    [TestClass]
    public class GetDentalOfficeDetailQueryHandlerTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IDentalOfficeRepository repository;
        private GetDentalOfficeDetailQueryHandler handler;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDentalOfficeRepository>();
            handler = new GetDentalOfficeDetailQueryHandler(repository);
        }

        [TestMethod]
        public async Task Handle_DentalOfficeExists_ReturnsIt()
        {
            var dentalOffice = new DentalOffice("Dental Office A");
            var id = dentalOffice.Id;
            var query = new GetDentalOfficeDetailQuery { Id = id };

            repository.GetDentalOfficeByIdAsync(id).Returns(dentalOffice);

            var result = await handler.Handle(query, default);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual("Dental Office A", result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_DentalOfficeDoesNotExists_Throws()
        {
            var id = Guid.NewGuid();
            var query = new GetDentalOfficeDetailQuery { Id = id };

            repository.GetDentalOfficeByIdAsync(id).Returns((DentalOffice?)null);

            await handler.Handle(query, default);
        }
    }
}
