using DotNetEnv;
using SmartStudy.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

var envPath = Path.Combine(
    Directory.GetCurrentDirectory(),
    ".env"
);
if (!File.Exists(envPath))
    throw new InvalidOperationException("Arquivo .env não encontrado!");

Env.Load(envPath);
var dbConnection =
    Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

if (string.IsNullOrWhiteSpace(dbConnection))
    throw new InvalidOperationException(
        "DB_CONNECTION_STRING não encontrada. Verifique se o arquivo .env está na pasta SmartStudy.API."
    );

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(dbConnection);
builder.Services.AddSwaggerDocumentation();

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
