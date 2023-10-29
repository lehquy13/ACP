using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ACP.Application.Contracts.Interfaces;
using ACP.Domain.Entities.Identities;
using ACP.Domain.Errors;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ACP.Infrastructure.Persistence.Authentication;

public class IdentityTokenClaimService : ITokenClaimsService
{
    private readonly IUserManager _userManager;
    private readonly JwtSettings _jwtSettings;

    public IdentityTokenClaimService(IUserManager userManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> GetTokenAsync(string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            throw new UserNotFoundException(userName);
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };

        //Create a list of string roles and add them to the claims

        claims.Add(new Claim(ClaimTypes.Role, user.IdentityRole.Name));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}