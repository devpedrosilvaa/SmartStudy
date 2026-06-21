using Microsoft.EntityFrameworkCore;
using SmartStudy.Application.Interfaces.Repositories;
using SmartStudy.Application.Interfaces.Security;
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
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<RegisterUserUseCase>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnection)
        {
            services.AddDbContext<ApplicationContextDb>(options =>
                options.UseSqlServer(dbConnection)
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
