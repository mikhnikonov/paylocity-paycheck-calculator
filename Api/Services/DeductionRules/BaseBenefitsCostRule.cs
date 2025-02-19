using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services.DeductionRules;

public class BaseBenefitsCostRule : IDeductionRule
{
    public decimal GetDeduction(Employee employee, DeductionConfig config)
    {
        return config.BaseBenefitsCost * 12;
    }
}