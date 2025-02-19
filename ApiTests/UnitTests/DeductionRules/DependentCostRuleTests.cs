using Api.Models;
using Api.Services.DeductionRules;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace Api.Tests.DeductionRules;

public class DependentCostRuleTests
{
    private readonly DependentCostRule _rule;

    public DependentCostRuleTests()
    {
        _rule = new DependentCostRule();
    }

    [Fact]
    public void GetDeduction_WithNoDependents_ShouldReturnZero()
    {
        // Arrange
        var employee = new Employee { Dependents = new List<Dependent>() };
        var config = new DeductionConfig { DependentCost = 600m };
        // Act
        var result = _rule.GetDeduction(employee, config);

        // Assert
        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData(1, 7200)]  // One dependent: $600 * 12 months
    [InlineData(2, 14400)] // Two dependents: ($600 * 2) * 12 months
    [InlineData(3, 21600)] // Three dependents: ($600 * 3) * 12 months
    public void GetDeduction_WithDependents_ShouldReturnCorrectAnnualCost(int dependentCount, decimal expectedCost)
    {
        // Arrange
        var employee = new Employee
        {
            Dependents = Enumerable.Range(1, dependentCount)
                .Select(i => new Dependent())
                .ToList()
        };
        var config = new DeductionConfig { DependentCost = 600m };

        // Act
        var result = _rule.GetDeduction(employee, config);

        // Assert
        Assert.Equal(expectedCost, result);
    }
}