using FS.FakeTwiter.Domain.Entities;
using FS.FakeTwiter.Infrastructure.DataAccess;
using System;

namespace FS.FakeTwiter.IntegrationTests.Helpers;

public static class TestDataSeeder
{
    public static void SeedTweets(ApplicationDbContext db)
    {
        db.Tweets.AddRange(
            new Tweet { UserId = "user-b", Content = "Tweet de seguido", CreatedAt = DateTime.UtcNow }
        );
        db.SaveChanges();
    }

    public static void SeedFollow(ApplicationDbContext db)
    {
        db.Follows.Add(new FollowRelation
        {
            FollowerId = "user-a",
            FolloweeId = "user-b"
        });

        db.SaveChanges();
    }
    
    public static void SeedScenario_TimelineWithFollowedTweets(ApplicationDbContext db)
    {
        SeedFollow(db);
        SeedTweets(db);
    }
}
