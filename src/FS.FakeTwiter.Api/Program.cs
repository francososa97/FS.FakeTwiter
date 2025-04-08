using FS.FakeTwiter.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using FS.FakeTwiter.Application.Interfaces.Tweets;
using FS.FakeTwiter.Infrastructure.UnitOfWork;
using FS.FakeTwitter.Domain.Interfaces;
using FS.FakeTwitter.Application.Interfaces;
using FS.FakeTwitter.Infrastructure.Repositories;
using FS.FakeTwitter.Infrastructure.Services;
using FS.FakeTwitter.Application.Interfaces.Follows;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using FS.FakeTwiter.Application.Interfaces.Users;
using FS.FakeTwitter.Application.Services;
using FS.FakeTwiter.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);
        ConfigureMediatR(builder);
        ConfigureDatabase(builder);
        ConfigureSwagger(builder);
        ConfigureJWT(builder);
        var app = builder.Build();

        ConfigureMiddleware(app);
        ConfigureRouting(app);

        app.Run();
    }


    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<ITweetRepository, TweetRepository>();
        builder.Services.AddScoped<ITweetService, TweetService>();
        builder.Services.AddScoped<IFollowRepository, FollowRepository>();
        builder.Services.AddScoped<IFollowService, FollowService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void ConfigureMediatR(WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(Program).Assembly,
            typeof(ITweetService).Assembly
        ));
    }

    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        var useInMemory = builder.Configuration.GetValue<bool>("UseInMemory");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (useInMemory)
                options.UseInMemoryDatabase("FakeTwitterDb");
            else
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
    }

    private static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseMiddleware<ApiKeyMiddleware>();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "FS.FakeTwitter API V1");
        });
    }

    private static void ConfigureRouting(WebApplication app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
    private static void ConfigureJWT(WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
                ValidateLifetime = true
            };
        });

    }
}


