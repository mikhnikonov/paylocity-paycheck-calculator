using Api.Models;
using Api.Services.DeductionRules;
using Xunit;

namespace Api.Tests.DeductionRules;

public class HighIncomeRuleTests
{
    private readonly HighIncomeRule _rule;

    public HighIncomeRuleTests()
    {
        _rule = new HighIncomeRule();
    }

    [Theory]
    [InlineData(79999.99, 0)] // Just under threshold
    [InlineData(80000, 0)] // Still less than threshold
    [InlineData(80001, 1600.02)] // At threshold (80000 * 0.02)
    [InlineData(100000, 2000)] // Above threshold (100000 * 0.02)
    public void GetDeduction_ShouldCalculateCorrectDeduction(decimal salary, decimal expectedDeduction)
    {
        var employee = new Employee { Salary = salary };
        var config = new DeductionConfig
        {
            HighIncomeSalaryThreshold = 80000m,
            HighIncomePercentage = 0.02m
        };
        var result = _rule.GetDeduction(employee, config);
        Assert.Equal(expectedDeduction, result);
    }
}