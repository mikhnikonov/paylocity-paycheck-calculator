using Api.Models;
using Api.Services;
using Api.Services.DeductionRules;
using Api.Services.Interfaces;
using Api.DataAccess.Interfaces;
using Api.Dtos.Paycheck;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Api.Tests.Services;

public class PaycheckCalculationServiceTests
{
    private readonly Mock<IDeductionConfigRepository> _configRepositoryMock;
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IEmployeeValidationService> _validationServiceMock;
    private readonly DeductionConfig _config;
    private readonly PaycheckCalculationService _service;
    private readonly List<IDeductionRule> _rules;

    public PaycheckCalculationServiceTests()
    {
        _configRepositoryMock = new Mock<IDeductionConfigRepository>();
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _validationServiceMock = new Mock<IEmployeeValidationService>();
        _config = new DeductionConfig { PaychecksPerYear = 24 };
        _rules = new List<IDeductionRule>();
        
        _service = new PaycheckCalculationService(
            _rules, 
            _configRepositoryMock.Object,
            _employeeRepositoryMock.Object,
            _validationServiceMock.Object
        );
        
        _configRepositoryMock.Setup(x => x.GetDeductionConfig())
            .ReturnsAsync(_config);
    }

    [Theory]
    [InlineData(120000, 12000)]
    [InlineData(60000, 6000)]
    public async Task CalculatePaycheck_ShouldCalculateCorrectAmounts(decimal salary, decimal annualDeductions)
    {
        // Arrange
        var employee = new Employee { Id = 1, Salary = salary };
        _employeeRepositoryMock.Setup(x => x.GetEmployeeById(1))
            .ReturnsAsync(employee);
            
        var mockRule = new Mock<IDeductionRule>();
        mockRule.Setup(r => r.GetDeduction(employee, _config))
            .Returns(annualDeductions);
        _rules.Add(mockRule.Object);

        // Act
        var result = await _service.CalculatePaycheck(1);

        // Assert
        Assert.Equal(salary / _config.PaychecksPerYear, result.GrossPay);
        Assert.Equal(annualDeductions / _config.PaychecksPerYear, result.Deductions);
        Assert.Equal((salary - annualDeductions) / _config.PaychecksPerYear, result.NetPay);
    }

    [Fact]
    public async Task CalculatePaycheck_WithPennies_ShouldHandleRoundingCorrectly()
    {
        // Arrange
        var employee = new Employee { Id = 1, Salary = 100000.99m };
        _employeeRepositoryMock.Setup(x => x.GetEmployeeById(1))
            .ReturnsAsync(employee);
            
        var mockRule = new Mock<IDeductionRule>();
        mockRule.Setup(r => r.GetDeduction(employee, _config))
            .Returns(10000.99m);
        _rules.Add(mockRule.Object);

        // Act
        var result = await _service.CalculatePaycheck(1);

        // Assert
        Assert.Equal(4166.89m, result.GrossPay);
        Assert.Equal(416.89m, result.Deductions);
        Assert.Equal(3750.00m, result.NetPay);
    }

    [Fact]
    public async Task CalculatePaycheck_WithMultipleRules_ShouldSumDeductions()
    {
        // Arrange
        var employee = new Employee { Id = 1, Salary = 120000 };
        _employeeRepositoryMock.Setup(x => x.GetEmployeeById(1))
            .ReturnsAsync(employee);
            
        var mockRule1 = new Mock<IDeductionRule>();
        var mockRule2 = new Mock<IDeductionRule>();
        
        mockRule1.Setup(r => r.GetDeduction(employee, _config)).Returns(6000);
        mockRule2.Setup(r => r.GetDeduction(employee, _config)).Returns(4000);
        
        _rules.AddRange(new[] { mockRule1.Object, mockRule2.Object });

        // Act
        var result = await _service.CalculatePaycheck(1);

        // Assert
        Assert.Equal(5000, result.GrossPay);
        Assert.Equal(416.82m, result.Deductions);
        Assert.Equal(4583.18m, result.NetPay);
    }

    [Fact]
    public async Task CalculatePaycheck_WithInvalidDependents_ShouldThrowException()
    {
        // Arrange
        var employee = new Employee 
        { 
            Id = 1,
            Salary = 120000,
            Dependents = new List<Dependent>
            {
                new() { Relationship = Relationship.Spouse },
                new() { Relationship = Relationship.DomesticPartner }
            }
        };

        _employeeRepositoryMock.Setup(x => x.GetEmployeeById(1))
            .ReturnsAsync(employee);

        _validationServiceMock.Setup(x => x.ValidateEmployee(employee))
            .Throws(new InvalidOperationException("Cannot have both spouse and domestic partner"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.CalculatePaycheck(1)
        );
    }
}