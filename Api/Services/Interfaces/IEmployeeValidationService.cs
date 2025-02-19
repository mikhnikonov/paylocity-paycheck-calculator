using Api.Models;

namespace Api.Services.Interfaces;

public interface IEmployeeValidationService
{
    void ValidateEmployee(Employee employee);
}