using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
using ClientApi.Data;
using ClientApi.Models;
using ClientApi.Services;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICommandeService, CommandeService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssemblyContaining<ClientDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CommandeDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurer Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
