using Fs.Framework.Application.Interfaces;
using FS.FakeTwiter.Infrastructure.Repositories;
using FS.FakeTwiter.Infrastructure.Services;
using FS.Framework.Infrastructure.DataAccess;
using FS.FakeTwiter.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using FS.FakeTwiter.Domain;
using FS.FakeTwiter.Application.Interfaces.Tweets;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITweetRepository, TweetRepository>();
builder.Services.AddScoped<ITweetService, TweetService>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddScoped<IFollowService, FollowService>();
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
