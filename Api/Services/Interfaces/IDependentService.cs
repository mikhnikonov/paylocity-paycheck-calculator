using Api.Dtos.Dependent;

namespace Api.Services.Interfaces;

public interface IDependentService
{
    Task<List<GetDependentDto>> GetAllDependents();
    Task<GetDependentDto?> GetDependentById(int id);
}