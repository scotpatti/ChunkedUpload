using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChunkedUpload.Services
{
    public class JwtTokenService
    {
        private readonly int _tokenLifetime = 30; // minutes

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly EnvSecrets _secrets;


        public JwtTokenService(UserManager<IdentityUser> userManager, IConfiguration configuration, EnvSecrets secrets)
        {
            _userManager = userManager;
            _configuration = configuration;
            _secrets = secrets;
        }

        public string GenerateJwtToken(ClaimsPrincipal principalUser)
        {
            var user = _userManager.GetUserAsync(principalUser).Result;
            if (user == null || user.Email == null) throw new Exception("User must be logged in!");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secrets.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _secrets.JwtIssuer,
                audience: _secrets.JwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenLifetime),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
