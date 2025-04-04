using FS.FakeTwiter.Domain.Entities;
using FS.Framework.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FS.FakeTwiter.Infrastructure.DataAccess;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Tweets.Any()) return;

        context.Tweets.Add(new Tweet
        {
            Id = Guid.NewGuid(),
            UserId = "user123",
            Content = "Hola mundo desde FakeTwitter",
            CreatedAt = DateTime.UtcNow
        });

        context.SaveChanges();
    }
}
