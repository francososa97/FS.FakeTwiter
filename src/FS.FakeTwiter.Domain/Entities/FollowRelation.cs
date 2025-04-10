namespace FS.FakeTwiter.Domain.Entities;

/// <summary>
/// Entidad que representa una relación de seguimiento entre dos usuarios.
/// </summary>
public class FollowRelation
{
    /// <summary>
    /// Identificador único de la relación.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// ID del usuario que sigue.
    /// </summary>
    public string FollowerId { get; set; } = string.Empty;

    /// <summary>
    /// ID del usuario seguido.
    /// </summary>
    public string FolloweeId { get; set; } = string.Empty;

    /// <summary>
    /// Fecha y hora en que se realizó el seguimiento.
    /// </summary>
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}
