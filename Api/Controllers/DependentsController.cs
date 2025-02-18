using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

/// <summary>
/// Controller for managing dependent-related operations
/// Handles retrieval of dependent information
/// </summary>
/// <remarks>
/// TODO: Add more robust error handling by implementing a common request handler pattern
/// Consider using a Result pattern or middleware for consistent error responses
/// TODO: Add detailed error messages and implement logging for better debugging and monitoring
/// - Log failed requests and error details
/// - Add correlation IDs for request tracking
/// - Include more descriptive error messages in responses
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentService _dependentService;
    public DependentsController(IDependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = await _dependentService.GetDependentById(id);

        if (dependent == null) {
            return NotFound();
        }

        var result = new ApiResponse<GetDependentDto>
        {
            Data = dependent,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = await _dependentService.GetAllDependents();
        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents,
            Success = true
        };

        return result;
    }
}
