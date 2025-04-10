namespace FS.FakeTwiter.Application.DTOs;

/// <summary>
/// Representa los datos de un usuario para exposición en la API.
/// </summary>
public class UserDto
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
}
