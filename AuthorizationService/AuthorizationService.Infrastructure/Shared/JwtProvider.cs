using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizationService.Infrastructure.Shared
{
    public class AccessTokenOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresDays { get; set; } = 0;
        public string CookieName { get; set; } = string.Empty;
    }

    public class RefreshTokenOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresMinutes { get; set; } = 0;
        public string CookieName { get; set; } = string.Empty;
    }

    public class JwtOptions
    {
        public AccessTokenOptions AccessToken { get; set; } = new();
        public RefreshTokenOptions RefreshToken { get; set;} = new();
    }

    public interface IJwtProvider
    {
        string GenerateAccessToken(Guid id);
        string GenerateRefreshToken(Guid id);
    }

    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        private static Claim[] CreateClaims(Guid id)
        {
            return
            [
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            ];
        }

        public string GenerateAccessToken(Guid id)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.AccessToken.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: CreateClaims(id),
                expires: DateTime.UtcNow.AddDays(_options.AccessToken.ExpiresDays),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(Guid id)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.RefreshToken.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: CreateClaims(id),
                expires: DateTime.UtcNow.AddMinutes(_options.RefreshToken.ExpiresMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
