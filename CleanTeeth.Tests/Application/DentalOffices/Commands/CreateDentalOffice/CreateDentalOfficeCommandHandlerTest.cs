using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Company.TestProject1;

[TestClass]
public class CreateDentalOfficeCommandHandlerTest
{
    private IDentalOfficeRepository _dentalOfficeRepository;
    private IUnitOfWork _unitOfWork;
    private CreateDentalOfficeCommandHandler _handler;

    [TestInitialize] // This method runs before each test
    public void Setup()
    {
        _dentalOfficeRepository = Substitute.For<IDentalOfficeRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new CreateDentalOfficeCommandHandler(_dentalOfficeRepository, _unitOfWork);
    }

    [TestMethod]
    public async Task Handle_ValidCommand_ReturnsDentalOfficeId()
    {
        var command = new CreateDentalOfficeCommand { Name = "Test Dental Office" };

        // Capture the DentalOffice instance passed to the repository
        DentalOffice? addedDentalOffice = null;
        _dentalOfficeRepository
            .AddDentalOfficeAsync(Arg.Do<DentalOffice>(x => addedDentalOffice = x))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        await _dentalOfficeRepository.Received(1).AddDentalOfficeAsync(Arg.Any<DentalOffice>());
        await _unitOfWork.Received(1).CommitAsync();

        // Assert against the captured instance
        Assert.IsNotNull(addedDentalOffice);
        Assert.AreEqual(addedDentalOffice!.Id, result);
    }

    [TestMethod]
    public async Task Handle_WhenThereIsAnError_WeRollback ()
    {
        var command = new CreateDentalOfficeCommand { Name = "Test Dental Office" };

        _dentalOfficeRepository.AddDentalOfficeAsync(Arg.Any<DentalOffice>()).Throws<Exception>();

        await Assert.ThrowsExceptionAsync<Exception>(async () =>
        {
            await _handler.Handle(command, CancellationToken.None);
        });

        await _unitOfWork.Received(1).RollbackAsync();
    }
}
