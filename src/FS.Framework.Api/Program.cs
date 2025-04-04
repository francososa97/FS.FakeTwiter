using FS.Framework.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using FS.FakeTwiter.Application.Interfaces.Tweets;
using FS.FakeTwiter.Infrastructure.UnitOfWork;
using FS.FakeTwitter.Domain.Interfaces;
using FS.FakeTwitter.Application.Interfaces;
using FS.FakeTwitter.Infrastructure.Repositories;
using FS.FakeTwitter.Infrastructure.Services;
using FS.FakeTwitter.Application.Interfaces.Follows;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITweetRepository, TweetRepository>();
builder.Services.AddScoped<ITweetService, TweetService>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(ITweetService).Assembly
));

var useInMemory = builder.Configuration.GetValue<bool>("UseInMemory");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (useInMemory)
        options.UseInMemoryDatabase("FakeTwitterDb");
    else
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly)
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FS.FakeTwitter API V1");
        c.RoutePrefix = string.Empty; // Esto hace que Swagger sea la página raíz

    });
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
