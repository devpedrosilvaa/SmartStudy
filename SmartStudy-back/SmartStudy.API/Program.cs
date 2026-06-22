using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SmartStudy.API.Extensions;
using SmartStudy.API.Middlewares;
using SmartStudy.Application.UseCases.Auth;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var envPath = Path.Combine(
    builder.Environment.ContentRootPath,
    ".env"
);

Console.WriteLine("ENV PATH: " + envPath);
Console.WriteLine("ENV EXISTS: " + File.Exists(envPath));

DotNetEnv.Env.Load(envPath);

builder.Configuration.AddEnvironmentVariables();

Console.WriteLine("DB ENV: " + Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
Console.WriteLine("JWT KEY ENV: " + Environment.GetEnvironmentVariable("Jwt__Key"));

Console.WriteLine("DB CONFIG: " + builder.Configuration["DB_CONNECTION_STRING"]);
Console.WriteLine("JWT KEY CONFIG: " + builder.Configuration["Jwt:Key"]);

var dbConnection =
    builder.Configuration["DB_CONNECTION_STRING"];

if (string.IsNullOrWhiteSpace(dbConnection))
    throw new InvalidOperationException(
        "DB_CONNECTION_STRING não encontrada.");

var jwtKey =
    builder.Configuration["Jwt:Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException(
        "Jwt:Key não encontrada.");

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructure(
    dbConnection,
    builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey))
            };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();