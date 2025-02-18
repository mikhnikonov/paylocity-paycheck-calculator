using Api.Models;

namespace Api.DataAccess.Interfaces;

public interface IDependentRepository
{
    Task<List<Dependent>> GetAllDependents();
    Task<Dependent?> GetDependentById(int id);
}