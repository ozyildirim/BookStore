using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.RefreshToken;

public class RefreshTokenCommand
{
    public string RefreshToken { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(
            x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now
        );
        if (user is not null)
        {
            TokenHandler tokenHandler = new TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);
            _context.SaveChanges();

            return token;
        }
        else
        {
            throw new InvalidOperationException("Valid token not found!");
        }
    }
}
