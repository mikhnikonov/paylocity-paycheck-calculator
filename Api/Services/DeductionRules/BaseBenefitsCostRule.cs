using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services.DeductionRules;

/// <summary>
/// Calculates the base benefits cost deduction for an employee
/// Applies a fixed monthly cost (BaseBenefitsCost) multiplied by 12 months
/// This is the baseline deduction that applies to all employees regardless of 
/// their salary or number of dependents
/// </summary>
public class BaseBenefitsCostRule : IDeductionRule
{
    public decimal GetDeduction(Employee employee, DeductionConfig config)
    {
        return config.BaseBenefitsCost * 12;
    }
}