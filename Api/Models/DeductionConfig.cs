namespace Api.Models;

public class DeductionConfig
{
    public decimal BaseBenefitsCost { get; set; }
    public decimal DependentCost { get; set; }
    public decimal HighIncomeSalaryThreshold { get; set; }
    public decimal HighIncomePercentage { get; set; }
    public decimal SeniorDependentCost { get; set; }
    public int SeniorAgeThreshold { get; set; }
    public int PaychecksPerYear { get; set; }
}