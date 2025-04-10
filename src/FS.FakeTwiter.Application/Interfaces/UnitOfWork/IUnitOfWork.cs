/// <summary>
/// Interface para coordinar la persistencia de múltiples repositorios.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Repositorio de tweets.
    /// </summary>
    ITweetRepository Tweets { get; }

    /// <summary>
    /// Repositorio de relaciones de seguimiento.
    /// </summary>
    IFollowRepository Follows { get; }

    /// <summary>
    /// Repositorio de usuarios.
    /// </summary>
    IUserRepository Users { get; }

    /// <summary>
    /// Guarda los cambios realizados en las entidades persistidas.
    /// </summary>
    Task<int> SaveChangesAsync();
}
