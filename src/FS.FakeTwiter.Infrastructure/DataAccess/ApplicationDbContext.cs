using FS.FakeTwiter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FS.FakeTwiter.Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tweet> Tweets { get; set; }
    public DbSet<FollowRelation> Follows { get; set; }

}
