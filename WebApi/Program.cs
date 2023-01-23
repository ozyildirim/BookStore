using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Services
    .AddAuthentication()
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Token:Issuer"],
            ValidAudience = configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])
            ),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddDbContext<BookStoreDbContext>(
    options => options.UseInMemoryDatabase(databaseName: "BookStoreDB")
);
builder.Services.AddScoped<IBookStoreDbContext>(
    provider => provider.GetService<BookStoreDbContext>()
);

// builder.Services
//     .AddEntityFrameworkNpgsql()
//     .AddDbContext<BookStoreDbContext>(
//         opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"))
//     );

builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     DataGenerator.Initialize(services);
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//Custom Exception Middleware
app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
