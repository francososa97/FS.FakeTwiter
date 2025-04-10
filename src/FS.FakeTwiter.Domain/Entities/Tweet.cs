namespace FS.FakeTwiter.Domain.Entities;

/// <summary>
/// Entidad que representa un tweet.
/// </summary>
public class Tweet
{
    /// <summary>
    /// Identificador único del tweet.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// ID del usuario que publicó el tweet.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Texto del tweet.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de creación del tweet.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
