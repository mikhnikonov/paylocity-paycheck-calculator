using Api.Models;
using Api.Dtos.Employee;
using Api.DataAccess.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;

namespace Api.Services;

/// <summary>
/// Service for managing employee data and operations
/// Handles employee retrieval and data mapping between domain models and DTOs
/// </summary>
/// <remarks>
/// TODO: Implement proper error handling across all data access operations
/// This should include:
/// - Logging of errors and access attempts
/// - Custom exceptions for different failure scenarios
/// - Retry policies for transient failures
/// - Proper error responses for API consumers
/// </remarks>
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IEmployeeValidationService _validation;

    public EmployeeService(IEmployeeRepository repository, IMapper mapper, IEmployeeValidationService validation)
    {
        _repository = repository;
        _mapper = mapper;
        _validation = validation;
    }

    public async Task<List<GetEmployeeDto>> GetAllEmployees()
    {
        var employees = await _repository.GetAllEmployees();
        return _mapper.Map<List<GetEmployeeDto>>(employees);
    }

    public async Task<GetEmployeeDto?> GetEmployeeById(int id)
    {
        var employee = await _repository.GetEmployeeById(id);
        if (employee != null) {
            _validation.ValidateEmployee(employee);
            return _mapper.Map<GetEmployeeDto>(employee);
        }
        return _mapper.Map<GetEmployeeDto>(employee);
    }
}