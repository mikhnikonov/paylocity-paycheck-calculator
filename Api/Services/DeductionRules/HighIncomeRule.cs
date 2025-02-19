using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services.DeductionRules;

/// <summary>
/// Calculates additional deduction for high-income employees
/// </summary>
/// <remarks>
/// TODO: Consider implementing salary history tracking and prorating for mid-year salary changes
/// Current implementation uses the current salary rate as annual salary
/// </remarks>
public class HighIncomeRule : IDeductionRule
{
    public decimal GetDeduction(Employee employee, DeductionConfig config)
    {
        return employee.Salary > config.HighIncomeSalaryThreshold 
            ? employee.Salary * config.HighIncomePercentage 
            : 0;
    }
}