using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Application.GenreOperations.Queries;
using WebApi.DBOperations;
using FluentValidation;
using WebApi.Application.UserOperations;
using WebApi.Application.UserOperations.CreateToken;
using WebApi.TokenOperations.Models;
using Microsoft.AspNetCore.Authorization;
using WebApi.Application.UserOperations.RefreshToken;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class UserController : ControllerBase
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _configuration;

    public UserController(
        ILogger<UserController> logger,
        IBookStoreDbContext context,
        IMapper mapper,
        IConfiguration configuration
    )
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserModel newUser)
    {
        CreateUserCommand command = new CreateUserCommand(_context, _mapper);
        command.Model = newUser;
        command.Handle();

        return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenModel model)
    {
        CreateTokenCommand command = new CreateTokenCommand(_context, _configuration);
        command.Model = model;

        var token = command.Handle();
        return token;
    }

    // [Authorize]
    // [HttpGet]
    // public IActionResult GetUsers() { }

    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string token)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
        command.RefreshToken = token;
        var resultToken = command.Handle();
        return resultToken;
    }
}
