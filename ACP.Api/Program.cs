using System.Reflection;
using ACP.Application;
using ACP.DependencyInjection;
using ACP.Infrastructure;
using ACP.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Assembly[] assemblies = new[]
{
    Assembly.GetCallingAssembly(),
    typeof(ACP.Application.DependencyInjection).Assembly
};

builder.Services
    .AddServiced(assemblies)
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
