using FS.FakeTwiter.Domain.Entities;

/// <summary>
/// Repositorio encargado de la persistencia de tweets.
/// </summary>
public interface ITweetRepository
{
    /// <summary>
    /// Agrega un nuevo tweet.
    /// </summary>
    Task AddAsync(Tweet tweet);

    /// <summary>
    /// Obtiene todos los tweets publicados por un usuario.
    /// </summary>
    Task<IEnumerable<Tweet>> GetByUserIdAsync(string userId);

    /// <summary>
    /// Obtiene los tweets publicados por un conjunto de usuarios.
    /// </summary>
    Task<IEnumerable<Tweet>> GetByUsersAsync(IEnumerable<string> userIds);
}
