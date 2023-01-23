using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations;

public class TokenHandler
{
    public IConfiguration Configuration { get; set; }

    public TokenHandler(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public Token CreateAccessToken(User user)
    {
        Token tokenModel = new Token();
        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])
        );

        SigningCredentials signingCredentials = new SigningCredentials(
            key: key,
            algorithm: SecurityAlgorithms.HmacSha256
        );

        tokenModel.ExpirationDate = DateTime.Now.AddMinutes(15);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: Configuration["Token:Issuer"],
            audience: Configuration["Token:Audience"],
            expires: tokenModel.ExpirationDate,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
        );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        // Token is created.
        tokenModel.AccessToken = handler.WriteToken(securityToken);
        tokenModel.RefreshToken = CreateRefreshToken();

        return tokenModel;
    }

    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
