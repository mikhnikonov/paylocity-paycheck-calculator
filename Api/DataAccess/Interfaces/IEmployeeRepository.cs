using Api.Models;

namespace Api.DataAccess.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllEmployees();
    Task<Employee?> GetEmployeeById(int id);
}