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
            // Calculate age at the end of this year
            int ageThisYear = calculationDate.Year - dependent.DateOfBirth.Year;

            // Only proceed if person is or becomes 50 this year
            if (ageThisYear >= 50)
            {
                DateTime birthDateThisYear = dependent.DateOfBirth.AddYears(calculationDate.Year - dependent.DateOfBirth.Year);
                
                // If they turn exactly 50 this year, prorate from their birth month
                if (ageThisYear == 50)
                {
                    int monthsAfterBirthday = 13 - birthDateThisYear.Month; // Months including birth month
                    annualCost += config.SeniorDependentCost * monthsAfterBirthday;
                }
                else
                {
                    // Over 50, apply full year cost
                    annualCost += config.SeniorDependentCost * 12;
                }
            }
        }

        return annualCost;
    }
}