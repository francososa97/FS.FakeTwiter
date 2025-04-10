namespace FS.FakeTwitter.Domain.Entities;

/// <summary>
/// Entidad que representa un usuario del sistema.
/// </summary>
public class User
{
    /// <summary>
    /// Identificador único del usuario.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nombre de usuario.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Indica si el usuario fue marcado como eliminado (soft delete).
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}
