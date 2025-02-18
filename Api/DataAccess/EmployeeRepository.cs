using Api.Models;
using Api.DataAccess.Interfaces;

/// <summary>
/// Repository for managing employee data
/// Provides access to employee information and their associated dependents
/// </summary>
/// <remarks>
/// Currently using in-memory data storage
/// TODO: Consider implementing persistent storage
/// TODO: Add caching for better performance
/// TODO: Implement optimized queries for dependent relationships
/// </remarks>
namespace Api.DataAccess
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employeeData = new()
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };

        public Task<List<Employee>> GetAllEmployees()
        {
            return Task.FromResult(_employeeData);
        }

        public Task<Employee?> GetEmployeeById(int id)
        {
            var employee = _employeeData.FirstOrDefault(emp => emp.Id == id);
            return Task.FromResult(employee);
        }
    }
}