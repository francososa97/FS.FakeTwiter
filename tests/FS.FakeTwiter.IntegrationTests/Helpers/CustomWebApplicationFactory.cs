using FS.FakeTwiter.Infrastructure.DataAccess;
using FS.FakeTwiter.Infrastructure.UnitOfWork;
using FS.FakeTwitter.Infrastructure.Repositories;
using FS.FakeTwitter.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            ConfigureInMemoryDatabase(services);
            RegisterApplicationServices(services);
            RegisterMediatR(services);
            InitializeDatabase(services);
        });
    }
    private void ConfigureInMemoryDatabase(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

        if (descriptor != null)
            services.Remove(descriptor);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("FakeTwitterTestDb"));
    }

    private void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddScoped<ITweetRepository, TweetRepository>();
        services.AddScoped<ITweetService, TweetService>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IFollowService, FollowService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private void RegisterMediatR(IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(
                typeof(TProgram).Assembly,
                typeof(ITweetService).Assembly));
    }

    private void InitializeDatabase(IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();

        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureCreated();
    }
}