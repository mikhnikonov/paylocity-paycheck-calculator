using Api.Models;
using Api.Services.DeductionRules;
using System;
using System.Collections.Generic;
using Xunit;

namespace Api.Tests.DeductionRules;

public class SeniorDependentRuleTests
{
    private readonly SeniorDependentRule _rule;
    private readonly int _currentYear = DateTime.Now.Year;
    private readonly decimal _monthlyCost = 200m;
    private const int SeniorAgeThreshold = 50;

    public SeniorDependentRuleTests()
    {
        _rule = new SeniorDependentRule();
    }

    [Theory]
    [InlineData(SeniorAgeThreshold - 1, 0)]     // Not senior yet
    [InlineData(SeniorAgeThreshold, 1400)]      // Turning 50 this year
    [InlineData(SeniorAgeThreshold + 1, 2400)]  // Already senior
    public void GetDeduction_WithSingleDependent_ShouldCalculateCorrectly(int age, decimal expectedCost)
    {
        // Arrange
        var birthYear = _currentYear - age;
        var birthDate = new DateTime(birthYear, 6, 1);
        var dependent = new Dependent { DateOfBirth = birthDate };
        var employee = new Employee { Dependents = new List<Dependent> { dependent } };
        var config = new DeductionConfig { SeniorDependentCost = _monthlyCost };

        // Act
        var result = _rule.GetDeduction(employee, config);

        // Assert
        Assert.Equal(expectedCost, result);
    }

    [Fact]
    public void GetDeduction_WithNullDependents_ShouldReturnZero()
    {
        var employee = new Employee { Dependents = null };
        var config = new DeductionConfig { SeniorDependentCost = _monthlyCost };

        var result = _rule.GetDeduction(employee, config);

        Assert.Equal(0, result);
    }

    [Fact]
    public void GetDeduction_WithMultipleSeniorDependents_ShouldSumCosts()
    {
        // Arrange
        var dependents = new List<Dependent>
        {
            new() { DateOfBirth = new DateTime(_currentYear - (SeniorAgeThreshold + 1), 3, 1) },
            new() { DateOfBirth = new DateTime(_currentYear - SeniorAgeThreshold, 3, 1) }
        };
        var employee = new Employee { Dependents = dependents };
        var config = new DeductionConfig { SeniorDependentCost = _monthlyCost };

        // Expected: (12 months * 200) + (10 months * 200) = 4400
        var expectedCost = 4400m;

        // Act
        var result = _rule.GetDeduction(employee, config);

        // Assert
        Assert.Equal(expectedCost, result);
    }

    [Theory]
    [InlineData(1, 2400)]    // January birth = 12 months
    [InlineData(6, 1400)]    // June birth = 7 months
    [InlineData(12, 200)]    // December birth = 1 month
    public void GetDeduction_TurningSeniorInDifferentMonths_ShouldProrate(
        int birthMonth, decimal expectedCost)
    {
        // Arrange
        var birthYear = _currentYear - SeniorAgeThreshold; // Turning 50 this year
        var birthDate = new DateTime(birthYear, birthMonth, 1); 
        var dependent = new Dependent { DateOfBirth = birthDate };
        var employee = new Employee { Dependents = new List<Dependent> { dependent } };
        var config = new DeductionConfig { SeniorDependentCost = _monthlyCost };

        // Act
        var result = _rule.GetDeduction(employee, config);

        // Assert
        Assert.Equal(expectedCost, result);
    }
}