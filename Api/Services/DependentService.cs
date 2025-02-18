using Api.Models;
using Api.Dtos.Dependent;
using Api.DataAccess.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;

namespace Api.Services;

public class DependentService : IDependentService
{
    private readonly IDependentRepository _repository;
    private readonly IMapper _mapper;

    public DependentService(IDependentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetDependentDto>> GetAllDependents()
    {
        var dependents = await _repository.GetAllDependents();
        return _mapper.Map<List<GetDependentDto>>(dependents);
    }

    public async Task<GetDependentDto?> GetDependentById(int id)
    {
        var dependent = await _repository.GetDependentById(id);
        return _mapper.Map<GetDependentDto>(dependent);
    }
}