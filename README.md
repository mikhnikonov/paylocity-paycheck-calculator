# Paylocity Benefits Calculator

A payroll deduction calculation system for payment providers.

## Overview
Calculates benefit deductions for employee paychecks based on predefined rules:
- Base benefits cost: $1,000 per year
- Dependent cost: $600 per dependent
- Senior dependent surcharge: Additional cost for dependents over 50
- High income surcharge: Extra 2% for salaries over $80,000

## Solution Architecture

### Controllers
- `EmployeeController`: Manages employee data and paycheck calculations
- `DependentController`: Handles dependent information and relationships
- `PaycheckController`: Processes paycheck calculations and deductions

### Services
- `EmployeeService`: Core employee management logic
- `DependentService`: Dependent relationship handling
- `PaycheckCalculationService`: Calculation engine for deductions
- `DeductionRules`: Individual rule implementations for different deduction types

### Data Access Layer
- `EmployeeRepository`: Manages employee records with mock data
- `DependentRepository`: Handles dependent records with mock data
- `DeductionConfigRepository`: Stores benefit calculation configurations

## System Requirements
- .NET 6.0 SDK
- Visual Studio 2022 or VS Code
- xUnit for testing

## Key Assumptions

### Employee Stability
- Fixed annual salary
- No dependent changes
- Full year employment

> Note: This system is designed for the ideal corporate employee who understands that life changes are 
> merely inconvenient disruptions to their perfectly planned career trajectory. Marriage? Children? 
> Salary negotiations? Such chaos has no place in a well-ordered fiscal year. True professionals 
> schedule major life events to coincide with the annual benefits enrollment period.

### Technical Implementation
- 26 pay periods per year
- Fixed annual calculations
- No mid-year adjustments

## Getting Started
1. Clone the repository
2. Install dependencies:
```bash
   dotnet restore
```
3. Run the API:
```bash
    cd PaylocityBenefitsCalculator/Api 
    dotnet run
```
4. Access Swagger UI at `https://localhost:7124/swagger`

## Testing
The solution includes unit tests to verify business rules and calculation logic.

### Running Tests
1. Navigate to the test project:
```bash
   cd PaylocityBenefitsCalculator/ApiTests
```

2. Run all tests:
```bash
   dotnet test
```   

### Test Coverage
- Business rule validations
- Deduction calculations
- Edge cases and boundary conditions
- Data access operations

## Implementation Notes

### Production Considerations
- Replace in-memory storage with proper database
- Add error handling and logging
- Implement security and authorization
- Add performance optimizations and caching
- Include configuration management

### Data Persistence
- Currently using in-memory data storage
- Should be replaced with proper database implementation
- Need to implement proper repository pattern
- Add data migration strategy

### Error Handling
- Data access error handling and retries
- Custom exceptions for business rule violations
- Logging of errors and calculation attempts
- Proper error responses for API consumers

### Configuration Management
- Config validation and fallback values
- Config caching strategies
- Dynamic config updates

### Performance Optimizations
- Caching of calculation results
- Batch processing capabilities
- Query optimizations

### Security
- Input validation
- Authorization rules
- Data access auditing

These aspects should be addressed before moving to production.
