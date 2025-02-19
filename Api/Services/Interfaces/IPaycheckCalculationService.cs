using Api.Dtos.Paycheck;

namespace Api.Services.Interfaces;

public interface IPaycheckCalculationService
{
    Task<GetPaycheckDto?> CalculatePaycheck(int employeeId);
}