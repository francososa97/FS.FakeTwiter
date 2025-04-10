namespace FS.FakeTwiter.Application.DTOs;

/// <summary>
/// Representa el resultado de una operación, indicando si fue exitosa y con un mensaje asociado.
/// </summary>
public class OperationResultDto
{
    /// <summary>
    /// Mensaje descriptivo del resultado.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Indica si la operación fue exitosa.
    /// </summary>
    public bool Success { get; set; } = true;
}
