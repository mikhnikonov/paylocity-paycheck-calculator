using Api.Dtos.Employee;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

/// <summary>
/// Controller for managing employee-related operations
/// Handles retrieval of employee information and their dependents
/// Note: Currently implements read-only operations. Create, Update, and Delete 
/// operations should be added for complete employee management
/// </summary>
/// <remarks>
/// TODO: Add more robust error handling by implementing a common request handler pattern
/// Consider using a Result pattern or middleware for consistent error responses
/// TODO: Add detailed error messages and implement logging for better debugging and monitoring
/// - Log failed requests and error details
/// - Add correlation IDs for request tracking
/// - Include more descriptive error messages in responses
/// TODO: Implement remaining CRUD operations:
/// - POST endpoint for creating new employees
/// - PUT endpoint for updating existing employees
/// - DELETE endpoint for removing employees
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = await _employeeService.GetEmployeeById(id);

        if (employee == null) {
            return NotFound();
        }

        var result = new ApiResponse<GetEmployeeDto>
        {
            Data = employee,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        var employees = await _employeeService.GetAllEmployees();
        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees,
            Success = true
        };

        return result;
    }
}
