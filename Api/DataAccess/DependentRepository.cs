using Api.Models;
using Api.DataAccess.Interfaces;

/// <summary>
/// Repository for managing dependent data
/// Provides access to dependent information from the data store
/// </summary>
/// <remarks>
/// Currently using in-memory data storage
/// TODO: Consider implementing persistent storage
/// TODO: Add caching for better performance
/// </remarks>
namespace Api.DataAccess
{
    public class DependentRepository : IDependentRepository
    {
        private readonly List<Dependent> _dependentData = new()
        {
            new()
            {
                Id = 1,
                EmployeeId = 2,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3),
            },
            new()
            {
                Id = 2,
                EmployeeId = 2,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23)
            },
            new()
            {
                Id = 3,
                EmployeeId = 2,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18)
            },
            new()
            {
                Id = 4,
                EmployeeId = 3,
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2)
            }
        };

        public Task<List<Dependent>> GetAllDependents()
        {
            return Task.FromResult(_dependentData);
        }

        public Task<Dependent?> GetDependentById(int id)
        {
            var dependent = _dependentData.FirstOrDefault(dep => dep.Id == id);
            return Task.FromResult(dependent);
        }
    }
}