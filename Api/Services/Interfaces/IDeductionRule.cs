using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IDeductionRule
    {
        public decimal GetDeduction(Employee employee, DeductionConfig config);
    }
}