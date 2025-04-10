using FS.FakeTwitter.Domain.Entities;

/// <summary>
/// Servicio de aplicación para la gestión de usuarios.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Obtiene todos los usuarios activos.
    /// </summary>
    Task<IEnumerable<User>> GetAllAsync();

    /// <summary>
    /// Obtiene un usuario por ID.
    /// </summary>
    Task<User?> GetByIdAsync(Guid id);

    /// <summary>
    /// Agrega un nuevo usuario.
    /// </summary>
    Task<int> AddAsync(User user);

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    Task<User> UpdateAsync(User user);

    /// <summary>
    /// Marca un usuario como eliminado.
    /// </summary>
    Task<int> DeleteAsync(Guid id);

    /// <summary>
    /// Verifica si un email ya está registrado.
    /// </summary>
    Task<bool> EmailExistsAsync(string email);
}
