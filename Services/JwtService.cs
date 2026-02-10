using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CRM.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace CRM.Services;
public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;


    public JwtService(IOptions<JwtOptions> options) => _jwtOptions = options.Value;
    
    public string GenereateJwtToken(string userName)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Email, userName),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey
                (
                    Encoding.UTF8.GetBytes(_jwtOptions.Key)), 
                    SecurityAlgorithms.HmacSha512Signature
                )};       
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}