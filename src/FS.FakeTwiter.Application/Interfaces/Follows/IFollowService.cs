/// <summary>
/// Servicio encargado de manejar la lógica de negocio para el seguimiento entre usuarios.
/// </summary>
public interface IFollowService
{
    /// <summary>
    /// Ejecuta la acción de seguir a otro usuario.
    /// </summary>
    Task FollowAsync(string followerId, string followeeId);

    /// <summary>
    /// Devuelve los seguidores de un usuario.
    /// </summary>
    Task<IEnumerable<string>> GetFollowersAsync(string userId);

    /// <summary>
    /// Devuelve a quién sigue un usuario.
    /// </summary>
    Task<IEnumerable<string>> GetFollowingAsync(string userId);

    /// <summary>
    /// Verifica si ya existe una relación de seguimiento.
    /// </summary>
    Task<bool> ExistsAsync(string followerId, string followeeId);
}
