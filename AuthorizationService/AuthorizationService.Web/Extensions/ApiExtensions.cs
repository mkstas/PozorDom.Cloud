using AuthorizationService.Application.Services;
using AuthorizationService.Domain.Interfaces.Repositories;
using AuthorizationService.Domain.Interfaces.Services;
using AuthorizationService.Infrastructure.Shared;
using AuthorizationService.Persistence;
using AuthorizationService.Persistence.Repositories;
using AuthorizationService.Web.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Web.Extensions
{
    public static class ApiExtensions
    {
        public static void UseGlobalExceptionHandler(
            this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalExceptionHandler>();
        }

        public static void AddDependencyInjection(
            this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void AddCorsConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>()
                ?? throw new InvalidOperationException("AllowedOrigins not configured.");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }

        public static void AddDatabaseContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(AuthorizationServiceDbContext))
                ?? throw new InvalidOperationException("Database connection string not configured.");

            services.AddDbContext<AuthorizationServiceDbContext>(
                options =>
                {
                    options.UseNpgsql(connectionString);
                });
        }
    }
}
