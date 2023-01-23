using AutoMapper;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.CreateToken;

public class CreateTokenCommand
{
    public CreateTokenModel Model { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CreateTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(
            x => x.Email == Model.Email && x.Password == Model.Password
        );

        if (user is not null)
        {
            // Create Token
            TokenHandler handler = new TokenHandler(_configuration);
            Token token = handler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(3);
            _context.SaveChanges();

            return token;
        }
        else
        {
            throw new InvalidOperationException("Email or Password is wrong!");
        }
    }
}

public class CreateTokenModel
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
