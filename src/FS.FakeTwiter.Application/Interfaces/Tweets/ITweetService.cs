/// <summary>
/// Servicio de aplicación encargado de la lógica de negocio relacionada a tweets.
/// </summary>
public interface ITweetService
{
    /// <summary>
    /// Publica un nuevo tweet.
    /// </summary>
    Task<Guid> PostTweetAsync(string userId, string content);

    /// <summary>
    /// Obtiene los tweets del usuario.
    /// </summary>
    Task<IEnumerable<string>> GetUserTweetsAsync(string userId);

    /// <summary>
    /// Obtiene el timeline del usuario (tweets de los usuarios que sigue).
    /// </summary>
    Task<IEnumerable<string>> GetTimelineAsync(string userId);
}
