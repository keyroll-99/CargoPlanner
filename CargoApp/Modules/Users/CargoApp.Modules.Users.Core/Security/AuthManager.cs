using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Abstraction.Clock;
using CargoApp.Core.Infrastructure.Auth;
using Microsoft.IdentityModel.Tokens;
using JsonWebToken = CargoApp.Core.Abstraction.Auth.JsonWebToken;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CargoApp.Modules.Users.Core.Security;

public class AuthManager : IAuthManager
{
    private readonly IClock _clock;
    private readonly AuthOptions _authOptions;
    private readonly SigningCredentials _signingCredentials;
    private readonly string _issuer;


    public AuthManager(IClock clock, AuthOptions authOptions)
    {
        _clock = clock;
        _authOptions = authOptions;
        _signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.IssuerSigningKey)),
                SecurityAlgorithms.HmacSha256);
        _issuer = authOptions.Issuer;
    }

    public JsonWebToken CreateToken(Guid userId, string email)
    {
        var now = _clock.Now();

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var expires = now.Add(_authOptions.Expiry);

        var jwt = new JwtSecurityToken(
            _issuer,
            claims: jwtClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: _signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return new JsonWebToken
        {
            UserId = userId,
            Email = email,
            Expires = expires,
            AccessToken = token
        };
    }
}