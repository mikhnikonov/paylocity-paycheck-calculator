using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services;

public class EmployeeValidationService : IEmployeeValidationService
{
    public void ValidateEmployee(Employee employee)
    {
        ValidateDependentRelationships(employee);
        // Add more validation methods as needed
    }

    private void ValidateDependentRelationships(Employee employee)
    {
        if (employee.Dependents == null || !employee.Dependents.Any())
            return;

        var spouseCount = employee.Dependents.Count(d => d.Relationship == Relationship.Spouse);
        var partnerCount = employee.Dependents.Count(d => d.Relationship == Relationship.DomesticPartner);

        if (spouseCount + partnerCount > 1)
        {
            throw new InvalidOperationException("An employee can have either a spouse or a domestic partner, but not both.");
        }
    }
}