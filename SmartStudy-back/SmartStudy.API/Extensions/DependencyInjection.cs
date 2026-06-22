using Microsoft.EntityFrameworkCore;
using SmartStudy.Application.Interfaces.Repositories;
using SmartStudy.Application.Interfaces.Security;
using SmartStudy.Application.UseCases.Auth;
using SmartStudy.Application.UseCases.Auth.Login;
using SmartStudy.Application.UseCases.Auth.Register;
using SmartStudy.Infrastructure;
using SmartStudy.Infrastructure.Persistence;
using SmartStudy.Infrastructure.Repositories;
using SmartStudy.Infrastructure.Security;

namespace SmartStudy.API.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<RegisterUserUseCase>();
            services.AddScoped<LoginUseCase>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(
         this IServiceCollection services,
         string dbConnection,
         IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContextDb>(options =>
                options.UseSqlServer(dbConnection)
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.Configure<JwtOptions>(
                configuration.GetSection("Jwt"));

            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
