using Api.Models;

namespace Api.DataAccess.Interfaces;

public interface IDeductionConfigRepository
{
    Task<DeductionConfig> GetDeductionConfig();
} 