using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ACP.Application.Contracts.DataTransferObjects.Authentications;
using ACP.Application.Contracts.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ACP.Infrastructure.Authentication;

public class IdentityTokenClaimService(IOptions<JwtSettings> jwtSettings) : ITokenClaimsService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GetTokenAsync(IdentityUserDto identityUserDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, identityUserDto.Id.ToString()),
            new(ClaimTypes.Name, identityUserDto.Name),
            new(ClaimTypes.Email, identityUserDto.Email),
            //Create a list of string roles and add them to the claims
            new Claim(ClaimTypes.Role, identityUserDto.Role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}