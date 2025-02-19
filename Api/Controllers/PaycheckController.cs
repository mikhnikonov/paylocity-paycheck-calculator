using Api.Dtos.Paycheck;
using Api.Models;
using Api.DataAccess.Interfaces;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

/// <summary>
/// Controller for managing employee paycheck calculations
/// Handles computation of deductions, gross pay, and net pay based on configured rules
/// Note: Currently implements basic paycheck calculation. Additional features like
/// historical paycheck data, custom pay periods, or tax calculations could be added
/// </summary>
/// <remarks>
/// TODO: Add more robust error handling by implementing a common request handler pattern
/// Consider using a Result pattern or middleware for consistent error responses
/// TODO: Add detailed error messages and implement logging for better debugging and monitoring
/// - Log calculation details and rule applications
/// - Add correlation IDs for request tracking
/// - Include detailed breakdown of deductions in responses
/// TODO: Consider additional paycheck-related features:
/// - Historical paycheck retrieval
/// - Custom pay period calculations
/// - Year-to-date totals
/// - Tax withholding calculations
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
public class PaycheckController : ControllerBase
{
    private readonly IPaycheckCalculationService _calculationService;
    private readonly IEmployeeRepository _employeeRepository;

    public PaycheckController(
        IPaycheckCalculationService calculationService,
        IEmployeeRepository employeeRepository
    )
    {
        _calculationService = calculationService;
        _employeeRepository = employeeRepository;
    }

    [SwaggerOperation(Summary = "Get employee's single paycheck")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> Get(int id)
    {
        var employee = await _employeeRepository.GetEmployeeById(id);

        if (employee == null)
            return NotFound();

        var paycheck = await _calculationService.CalculatePaycheck(employee);

        var result = new ApiResponse<GetPaycheckDto>
        {
            Data = paycheck,
            Success = true
        };

        return result;
    }
}