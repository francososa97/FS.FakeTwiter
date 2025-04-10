using FS.FakeTwiter.Domain.Entities;


/// <summary>
/// Repositorio encargado de la persistencia de relaciones de seguimiento entre usuarios.
/// </summary>
public interface IFollowRepository
{
    /// <summary>
    /// Agrega una nueva relación de seguimiento.
    /// </summary>
    Task AddAsync(FollowRelation relation);

    /// <summary>
    /// Obtiene los IDs de los seguidores del usuario.
    /// </summary>
    Task<IEnumerable<string>> GetFollowersAsync(string userId);

    /// <summary>
    /// Obtiene los IDs de los usuarios a los que sigue el usuario.
    /// </summary>
    Task<IEnumerable<string>> GetFollowingAsync(string userId);

    /// <summary>
    /// Verifica si una relación de seguimiento ya existe.
    /// </summary>
    Task<bool> ExistsAsync(string followerId, string followeeId);
}
