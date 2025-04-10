namespace FS.FakeTwiter.Application.Interfaces.Cache;

/// <summary>
/// Abstracción para manejar operaciones de caching relacionadas con relaciones de seguidores y timelines.
/// </summary>
public interface ICacheHelper
{
    /// <summary>
    /// Obtiene desde el cache la lista de seguidores de un usuario, o la genera usando la función si no está cacheada.
    /// </summary>
    /// <param name="userId">ID del usuario del cual se quieren obtener los seguidores.</param>
    /// <param name="factory">Función para obtener los datos si no están en caché.</param>
    /// <returns>Lista de IDs de seguidores.</returns>
    Task<IEnumerable<string>> GetOrSetFollowersAsync(string userId, Func<Task<IEnumerable<string>>> factory);

    /// <summary>
    /// Obtiene desde el cache la lista de usuarios seguidos por un usuario, o la genera usando la función si no está cacheada.
    /// </summary>
    /// <param name="userId">ID del usuario del cual se quieren obtener los seguidos.</param>
    /// <param name="factory">Función para obtener los datos si no están en caché.</param>
    /// <returns>Lista de IDs de usuarios seguidos.</returns>
    Task<IEnumerable<string>> GetOrSetFollowingAsync(string userId, Func<Task<IEnumerable<string>>> factory);

    /// <summary>
    /// Elimina del cache la lista de seguidores de un usuario.
    /// </summary>
    /// <param name="userId">ID del usuario del cual se debe invalidar el cache de seguidores.</param>
    void RemoveFollowersCache(string userId);

    /// <summary>
    /// Elimina del cache la lista de usuarios seguidos por un usuario.
    /// </summary>
    /// <param name="userId">ID del usuario del cual se debe invalidar el cache de seguidos.</param>
    void RemoveFollowingCache(string userId);

    /// <summary>
    /// Obtiene desde el cache el timeline de un usuario, o lo genera usando la función si no está cacheado.
    /// </summary>
    /// <param name="userId">ID del usuario del cual se quiere obtener el timeline.</param>
    /// <param name="factory">Función para generar el timeline si no está cacheado.</param>
    /// <returns>Lista de strings representando los tweets.</returns>
    Task<IEnumerable<string>> GetOrSetTimelineAsync(string userId, Func<Task<IEnumerable<string>>> factory);

    /// <summary>
    /// Elimina del cache el timeline de un usuario.
    /// </summary>
    /// <param name="userId">ID del usuario del cual se debe invalidar el timeline.</param>
    void RemoveTimelineCache(string userId);
}
