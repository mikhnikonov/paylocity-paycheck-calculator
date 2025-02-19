using Api.Models;
using Api.DataAccess.Interfaces;
using Api.Dtos.Paycheck;
using Api.Services.Interfaces;

namespace Api.Services;

/// <summary>
/// Service for calculating employee paycheck details
/// Handles deduction calculations, gross and net pay computations based on configured rules
/// </summary>
/// <remarks>
/// TODO: Implement proper error handling across all operations
/// This should include:
/// - Logging of errors and calculation attempts
/// - Custom exceptions for configuration and calculation failures
/// - Retry policies for transient config access failures
/// - Proper error responses for invalid calculations
/// </remarks>
public class PaycheckCalculationService : IPaycheckCalculationService
{
    private readonly IEnumerable<IDeductionRule> _deductionRules;
    private readonly IDeductionConfigRepository _configRepository;

    public PaycheckCalculationService(
        IEnumerable<IDeductionRule> deductionRules,
        IDeductionConfigRepository configRepository)
    {
        _deductionRules = deductionRules;
        _configRepository = configRepository;
    }

    public async Task<GetPaycheckDto> CalculatePaycheck(Employee employee)
    {
        DeductionConfig config = await _configRepository.GetDeductionConfig();
        decimal annualDeductions = _deductionRules.Sum(rule => rule.GetDeduction(employee, config));
        decimal annualGross = employee.Salary;

        // Using Math.Floor ensures we never round up and potentially overpay
        // Example: $1000.456789 per period
        // Math.Round would give: $1000.46 (rounding up)
        // Math.Floor gives: $1000.45 (always rounding down)
        // This is a common practice in financial calculations to avoid overpayment
        decimal baseDeductions = Math.Floor(annualDeductions / config.PaychecksPerYear * 100) / 100;
        decimal baseGross = Math.Floor(annualGross / config.PaychecksPerYear * 100) / 100;

        // Any remaining amount due to rounding down is added to the final paycheck
        // This ensures the total annual amount is exact
        decimal deductionsRemainder = annualDeductions - (baseDeductions * config.PaychecksPerYear);
        decimal grossRemainder = annualGross - (baseGross * config.PaychecksPerYear);

        decimal finalDeductions = baseDeductions + deductionsRemainder;
        decimal finalGross = baseGross + grossRemainder;
        
        return new GetPaycheckDto
        {
            GrossPay = finalGross,
            Deductions = finalDeductions,
            NetPay = finalGross - finalDeductions
        };
    }
}