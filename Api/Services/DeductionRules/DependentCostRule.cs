using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services.DeductionRules;

/// <summary>
/// Calculates annual deduction cost for employee dependents
/// </summary>
/// <remarks>
/// Current implementation assumes dependents remain constant throughout the year.
/// Changes in dependents (births, deaths, marriages, divorces) during the year
/// are not prorated. The full annual cost is calculated based on the current
/// number of dependents at calculation time.
/// </remarks>
public class DependentCostRule : IDeductionRule
{
    public decimal GetDeduction(Employee employee, DeductionConfig config)
    {
        if (employee.Dependents == null || !employee.Dependents.Any())
            return 0;

        return employee.Dependents.Count * config.DependentCost * 12;
    }
}