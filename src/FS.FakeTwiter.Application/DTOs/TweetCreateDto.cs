namespace FS.FakeTwitter.Application.DTOs;

/// <summary>
/// Representa el contenido necesario para crear un nuevo tweet.
/// </summary>
public class TweetCreateDto
{
    /// <summary>
    /// ID del usuario que publica el tweet.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Contenido del tweet.
    /// </summary>
    public string Content { get; set; } = string.Empty;
}
