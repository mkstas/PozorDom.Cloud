using AuthorizationService.Infrastructure.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AuthorizationService.Web.Extensions
{
    public static class AuthExtensions
    {
        public static void AddJwtConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions))
                ?? throw new InvalidOperationException("JwtOptions not configured.");

            services.Configure<JwtOptions>(jwtOptions);
        }

        public static void AddApiAuthentification(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>()
                ?? throw new InvalidOperationException("JwtOptions not configured.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.AccessToken.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[jwtOptions.AccessToken.CookieName];
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User Id claim not found.");

            return Guid.Parse(userIdClaim.Value);
        }
    }
}
