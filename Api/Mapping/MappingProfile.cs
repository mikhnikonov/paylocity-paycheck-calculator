using Api.Models;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using AutoMapper;

namespace Api.Mapping;

public class AutoMapperProfile : Profile
{
   public AutoMapperProfile()
    {
        // Dependent mappings
        CreateMap<Dependent, GetDependentDto>();
        CreateMap<GetDependentDto, Dependent>();

        // Employee mappings
        CreateMap<Employee, GetEmployeeDto>();
        CreateMap<GetEmployeeDto, Employee>();
    }
}