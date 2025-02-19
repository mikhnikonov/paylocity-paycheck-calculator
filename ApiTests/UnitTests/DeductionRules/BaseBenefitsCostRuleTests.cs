using Api.Models;
using Api.Services.DeductionRules;
using Xunit;

namespace Api.Tests.DeductionRules;

public class BaseBenefitsCostRuleTests
{
    private readonly BaseBenefitsCostRule _rule;

    public BaseBenefitsCostRuleTests()
    {
        _rule = new BaseBenefitsCostRule();
    }

    [Fact]
    public void GetDeduction_ShouldReturnAnnualBaseCost()
    {
        // Arrange
        var employee = new Employee();
        var config = new DeductionConfig { BaseBenefitsCost = 1000m };
        var expectedAnnualCost = config.BaseBenefitsCost * 12;

        // Act
        var result = _rule.GetDeduction(employee, config);

        // Assert
        Assert.Equal(expectedAnnualCost, result);
    }
}