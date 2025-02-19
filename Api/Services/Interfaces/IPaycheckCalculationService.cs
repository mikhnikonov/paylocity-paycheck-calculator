using Api.Models;
using Api.Dtos.Paycheck;

namespace Api.Services.Interfaces;

public interface IPaycheckCalculationService
{
    Task<GetPaycheckDto> CalculatePaycheck(Employee employee);
}