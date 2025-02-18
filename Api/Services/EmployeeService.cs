using Api.Models;
using Api.Dtos.Employee;
using Api.DataAccess.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;

namespace Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetEmployeeDto>> GetAllEmployees()
    {
        var employees = await _repository.GetAllEmployees();
        return _mapper.Map<List<GetEmployeeDto>>(employees);
    }

    public async Task<GetEmployeeDto?> GetEmployeeById(int id)
    {
        var employee = await _repository.GetEmployeeById(id);
        return _mapper.Map<GetEmployeeDto>(employee);
    }
}