using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services.DeductionRules;

/// <summary>
/// Calculates additional deduction costs for senior dependents.
/// For dependents who turn senior age (50) during the year:
/// - Deduction starts from their birthday month
/// - Cost is prorated for remaining months of the year
/// - Each month's cost is added to the annual total
/// Example:
/// If dependent turns 50 in March:
/// - Deduction applies for 10 months (March-December)
/// - Annual cost = monthly rate × 10 months
/// </summary>
public class SeniorDependentRule : IDeductionRule
{
    public decimal GetDeduction(Employee employee, DeductionConfig config)
    {
        if (employee.Dependents == null)
            return 0;

        // Strip time portion for date-only comparisons
        DateTime calculationDate = DateTime.Now.Date;
        decimal annualCost = 0;

        foreach (Dependent dependent in employee.Dependents)
        {
            // Calculate what date the birthday falls on this year
            DateTime birthDateThisYear = dependent.DateOfBirth.AddYears(calculationDate.Year - dependent.DateOfBirth.Year);
            
            // Check if birthday already happened this year
            if (birthDateThisYear.Year == calculationDate.Year && 
                birthDateThisYear.Month <= calculationDate.Month)
            {
                // Calculate months from birthday to end of year (including birthday month)
                int monthsInYear = calculationDate.Month - birthDateThisYear.Month + 1;
                
                // Calculate partial year cost for this dependent
                annualCost += config.SeniorDependentCost * monthsInYear;
            }
        }

        return annualCost;
    }
}