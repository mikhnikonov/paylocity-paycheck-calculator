using Microsoft.OpenApi.Models;
using Api.DataAccess;
using Api.DataAccess.Interfaces;
using Api.Services;
using Api.Services.Interfaces;
using Api.Services.DeductionRules;
using Api.Mapping;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register repositories
builder.Services.AddScoped<IDependentRepository, DependentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDeductionConfigRepository, DeductionConfigRepository>();

// Register services
builder.Services.AddScoped<IDependentService, DependentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPaycheckCalculationService, PaycheckCalculationService>();
builder.Services.AddSingleton<IEmployeeValidationService, EmployeeValidationService>();

// Register deduction rules
builder.Services.AddSingleton<IDeductionRule, BaseBenefitsCostRule>();
builder.Services.AddSingleton<IDeductionRule, DependentCostRule>();
builder.Services.AddSingleton<IDeductionRule, HighIncomeRule>();
builder.Services.AddSingleton<IDeductionRule, SeniorDependentRule>();

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

// adding services to be used via injection
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
