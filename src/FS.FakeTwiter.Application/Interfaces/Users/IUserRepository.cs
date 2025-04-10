using FS.FakeTwitter.Domain.Entities;

/// <summary>
/// Repositorio encargado de la persistencia de usuarios.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Obtiene todos los usuarios que no están eliminados.
    /// </summary>
    Task<IEnumerable<User>> GetAllAsync();

    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    Task<User?> GetByIdAsync(Guid id);

    /// <summary>
    /// Agrega un nuevo usuario.
    /// </summary>
    Task AddAsync(User user);

    /// <summary>
    /// Actualiza los datos de un usuario.
    /// </summary>
    User Update(User user);

    /// <summary>
    /// Guarda los cambios pendientes.
    /// </summary>
    Task SaveChangesAsync();

    /// <summary>
    /// Realiza un soft delete del usuario.
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Verifica si ya existe un usuario con el email dado.
    /// </summary>
    Task<bool> EmailExistsAsync(string email);
}
