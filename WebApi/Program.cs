using FluentValidation;
using FluentValidation.AspNetCore;
using Infra.Config;
using Infra.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Controllers.Validators;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PesquisaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEfRepositories();

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
    });

//Add validators

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddFluentValidationAutoValidation();

// Registra automaticamente todos os validators
builder.Services.AddValidatorsFromAssemblyContaining<GetEmpresaByIdValidator>();

// Registra erros de validação

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
        .Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage)
        .ToList();

        var response = new ErrorResponse
        {
            Message = "Erro de validação",
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pesquisa API",
        Version = "v1",
        Description = "API do projeto Pesquisa"
    });

    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pesquisa API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
