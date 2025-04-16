using FS.FakeTwiter.Application.Features.Tweet.Queries.GetUserTweets;
using FS.FakeTwitter.Application.Features.Tweets.Queries;

namespace FS.FakeTwiter.UnitTests.Application.Validator;

public class GetUserTweetsTest
{
    [Fact]
    public void GetUserTweetsQueryValidator_Should_Fail_On_EmptyUserId()
    {
        var validator = new GetUserTweetsQueryValidator();
        var result = validator.Validate(new GetUserTweetsQuery { UserId = "" });
        Assert.False(result.IsValid);
    }
}
