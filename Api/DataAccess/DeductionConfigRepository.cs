using Api.DataAccess.Interfaces;
using Api.Models.DeductionConfigKey;

namespace Api.DataAccess;

/// <summary>
/// Repository for managing benefit deduction configurations
/// Note: In production environment, these values should be provided by external regulatory service
/// </summary>
/// <remarks>
/// TODO: Replace hardcoded values with external service integration
/// - Values are subject to regulatory compliance and updates
/// - Changes require audit trail and proper change management
/// - May vary by jurisdiction/region
/// - Owned by compliance/finance departments
/// - Consider implementing proper versioning and effective dates
/// </remarks>
public class DeductionConfigRepository : IDeductionConfigRepository
{
    private readonly Dictionary<DeductionConfigKeys, decimal> _deductionConfig = new()
    {
        { BaseBenefitsCost, 1000m },
        { DependentCost, 600m },
        { HighIncomeSalaryThreshold, 80000m },
        { HighIncomePercentage, 0.02m },
        { SeniorDependentCost, 200m },
        { SeniorAgeThreshold, 50 }
    };

    public Task<Dictionary<string, decimal>> GetDeductionConfig()
    {
        return Task.FromResult(_deductionConfig);
    }
}