using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Companies.Core.Commands.AddWorker;
using CargoApp.Modules.Companies.Core.Commands.CreateCompany;
using CargoApp.Modules.Companies.Core.Entities;
using CargoApp.Modules.Companies.Core.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Companies.Tests;

public class CreateCompanyCommandHandlerTests
{
    private readonly ICompanyRepository _companyRepository = Substitute.For<ICompanyRepository>();
    private readonly IEmployeeRepository _employeeRepository = Substitute.For<IEmployeeRepository>();
    private readonly IPolicy<CreateEmployeeCommand> _mockPolicy = Substitute.For<IPolicy<CreateEmployeeCommand>>();
    private readonly IClock _clock = Substitute.For<IClock>();
    private readonly IContext _context = Substitute.For<IContext>();

    private readonly CreateEmployeeCommandHandler _handler;

    public CreateCompanyCommandHandlerTests()
    {
        _handler = new CreateEmployeeCommandHandler(
            _companyRepository,
            _employeeRepository,
            new List<IPolicy<CreateEmployeeCommand>>() { _mockPolicy },
            _clock,
            _context
        );
    }

    [Fact]
    public async Task WhenPolicyReturnError_ThenReturnError()
    {
        // Arrange
        _mockPolicy.ErrorMessage.Returns("error");
        _mockPolicy.IsApplicable(Arg.Any<CreateEmployeeCommand>()).Returns(true);
        _mockPolicy.IsValidAsync(Arg.Any<CreateEmployeeCommand>()).Returns(false);

        // Act
        var result = await _handler.Handle(new CreateEmployeeCommand("name", "surname", "email",
            WorkingPositionEnum.Dispatcher, Guid.NewGuid()), new CancellationToken(false));
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("error");
        await _employeeRepository.DidNotReceive().AddAsync(Arg.Any<Employee>());
    }
    
    [Fact]
    public async Task WhenCompanyDoesntExist_ThenReturnError()
    {
        // Arrange
        _mockPolicy.ErrorMessage.Returns("error");
        _mockPolicy.IsApplicable(Arg.Any<CreateEmployeeCommand>()).Returns(true);
        _mockPolicy.IsValidAsync(Arg.Any<CreateEmployeeCommand>()).Returns(true);
        _companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(null as Company);

        // Act
        var result = await _handler.Handle(new CreateEmployeeCommand("name", "surname", "email",
            WorkingPositionEnum.Dispatcher, Guid.NewGuid()), new CancellationToken(false));
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Company not found");
        await _employeeRepository.DidNotReceive().AddAsync(Arg.Any<Employee>());
    } 
    
    [Fact]
    public async Task WhenDataAreCorrect_ThenAddNewEmployee()
    {
        // Arrange
        _mockPolicy.ErrorMessage.Returns("error");
        _mockPolicy.IsApplicable(Arg.Any<CreateEmployeeCommand>()).Returns(true);
        _mockPolicy.IsValidAsync(Arg.Any<CreateEmployeeCommand>()).Returns(true);
        _companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new Company(Guid.NewGuid(), DateTime.Now, "name", CompanyType.Delivery));

        // Act
        var result = await _handler.Handle(new CreateEmployeeCommand("name", "surname", "email",
            WorkingPositionEnum.Dispatcher, Guid.NewGuid()), new CancellationToken(false));
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        await _employeeRepository.Received(1).AddAsync(Arg.Any<Employee>());
    }
}