using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Companies.Core.Commands.CreateCompany;
using CargoApp.Modules.Companies.Core.Entities;
using CargoApp.Modules.Companies.Core.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Xunit;
using Company = CargoApp.Module.Contracts.Companies.DTO.Company;

namespace CargoApp.Modules.Companies.Tests;

public class CreateCompanyTests
{
    private IPolicy<CreateCompanyCommand> _mockPolicy = Substitute.For<IPolicy<CreateCompanyCommand>>();
    private ICompanyRepository _companyRepository = Substitute.For<ICompanyRepository>();
    private IClock _clock = Substitute.For<IClock>();

    private CreateCompanyCommandHandler _handler;

    public CreateCompanyTests()
    {
        _handler = new CreateCompanyCommandHandler(
            new List<IPolicy<CreateCompanyCommand>>() { _mockPolicy },
            _companyRepository,
            _clock
        );
    }

    [Fact]
    public async Task WhenPolicyReturnError_ThenReturnError()
    {
        // Arrange
        _mockPolicy.ErrorMessage.Returns("error");
        _mockPolicy.IsApplicable(Arg.Any<CreateCompanyCommand>()).Returns(true);
        _mockPolicy.IsValidAsync(Arg.Any<CreateCompanyCommand>()).Returns(false);

        // Act
        var result = await _handler.Handle(new CreateCompanyCommand("test", CompanyType.Delivery),
            new CancellationToken(false));

        // Arrange
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("error");
    }

    [Fact]
    public async Task WhenPolicyReturnSuccess_ThenAddNewCompany()
    {
        // Arrange
        _mockPolicy.ErrorMessage.Returns("error");
        _mockPolicy.IsApplicable(Arg.Any<CreateCompanyCommand>()).Returns(true);
        _mockPolicy.IsValidAsync(Arg.Any<CreateCompanyCommand>()).Returns(true);

        // Act
        var result = await _handler.Handle(new CreateCompanyCommand("test", CompanyType.Delivery),
            new CancellationToken(false));

        // Arrange
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();

        await _companyRepository.Received(1).AddAsync(Arg.Is<Core.Entities.Company>(x => x.Name == "test"));
    }
}