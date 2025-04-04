using FS.FakeTwitter.Application.Interfaces.Follows;
using FS.FakeTwitter.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Fs.Framework.Application.Interfaces;

namespace FS.FakeTwitter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowController : ControllerBase
{
    private readonly IFollowService _followService;

    public FollowController(IFollowService followService)
    {
        _followService = followService;
    }

    [HttpPost]
    public async Task<IActionResult> FollowUser([FromBody] FollowDto follow)
    {
        await _followService.FollowAsync(follow.FollowerId, follow.FolloweeId);
        return Ok();
    }

    [HttpGet("followers/{userId}")]
    public async Task<IActionResult> GetFollowers(string userId)
    {
        var followers = await _followService.GetFollowersAsync(userId);
        return Ok(followers);
    }

    [HttpGet("following/{userId}")]
    public async Task<IActionResult> GetFollowing(string userId)
    {
        var following = await _followService.GetFollowingAsync(userId);
        return Ok(following);
    }
}
