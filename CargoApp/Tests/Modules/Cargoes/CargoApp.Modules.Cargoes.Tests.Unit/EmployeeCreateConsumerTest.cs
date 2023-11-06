using CargoApp.Modules.Cargoes.Application.Driver;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Contracts.Events.Companies;
using CargoApp.Modules.Contracts.Events.Companies.Enums;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Cargoes.Tests.Unit;

public class EmployeeCreateConsumerTest
{
    private readonly CargoApp.Core.Abstraction.Services.IServiceProvider _serviceProviderMock = Substitute.For<CargoApp.Core.Abstraction.Services.IServiceProvider>();
    private readonly ICompanyRepository _companyRepositoryMock = Substitute.For<ICompanyRepository>();

    private readonly EmployeeCreateConsumer _employeeCreateConsumer;
    
    public EmployeeCreateConsumerTest()
    {
        _serviceProviderMock.GetService<ICompanyRepository>().Returns(_companyRepositoryMock);
        _employeeCreateConsumer = new EmployeeCreateConsumer(_serviceProviderMock);
    }

    [Fact]
    public async Task When_Create_Employee_Other_Than_Driver_Then_Do_Nothing()
    {
        // Arrange
        var @event = new EmployeeCreateEvent(Guid.NewGuid(), Guid.NewGuid(), "test", "test", "test",
            WorkingPositionEnum.Dispatcher);
        
        // Act
        await _employeeCreateConsumer.Process(@event);
        
        // Assert
        await _companyRepositoryMock.DidNotReceive().GetByCompanyId(Arg.Any<Guid>());
    }

    [Fact]
    public async Task When_Create_Employee_Witch_Is_Driver_Then_Create_Driver()
    {
        // Arrange
        var @event = new EmployeeCreateEvent(Guid.NewGuid(), Guid.NewGuid(), "test", "test", "test",
            WorkingPositionEnum.Driver);

        _companyRepositoryMock.GetByCompanyId(Arg.Any<Guid>()).Returns(new Company
        {
            Id = Guid.NewGuid()
        });
        
        // Act
        await _employeeCreateConsumer.Process(@event);
        
        // Assert
        await _companyRepositoryMock.Received().UpdateAsync(Arg.Any<Company>());
    }
    
}