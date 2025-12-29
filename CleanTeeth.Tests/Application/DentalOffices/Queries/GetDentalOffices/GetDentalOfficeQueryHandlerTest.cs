using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeQuery;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using NSubstitute;

namespace Company.TestProject1;

[TestClass]
public class GetDentalOfficeQueryHandlerTest
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IDentalOfficeRepository repository;
        private GetDentalOfficeQueryHandler handler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


        [TestInitialize]
        public void Setup()
        {
            repository = Substitute.For<IDentalOfficeRepository>();
            handler = new GetDentalOfficeQueryHandler(repository);
        }

        [TestMethod]
        public async Task Handle_WhenThereAreDentalOffices_ReturnsListOfThem()
        {
            var dentalOffices = new List<DentalOffice>
            {
                new DentalOffice("Dental Office A"),
                new DentalOffice("Dental Office B")
            };

            repository.GetAllDentalOfficesAsync().Returns(dentalOffices);

            var expected = dentalOffices.Select(d => new GetDentalOfficesDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();

            var result = await handler.Handle(new GetDentalOfficeQuery(), default);

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, result[i].Id);
                Assert.AreEqual(expected[i].Name, result[i].Name);
            }
        }

        [TestMethod]
        public async Task Handle_WhenThereAreNoDentalOffices_ItReturnsAnEmptyList()
        {
            repository.GetAllDentalOfficesAsync().Returns(new List<DentalOffice>());

            var result = await handler.Handle(new GetDentalOfficeQuery(), default);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
}
