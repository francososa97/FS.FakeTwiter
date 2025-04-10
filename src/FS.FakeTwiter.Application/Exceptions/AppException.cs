namespace FS.FakeTwitter.Application.Exceptions;

/// <summary>
/// Excepción personalizada que permite devolver un status code HTTP y un mensaje.
/// </summary>
public class AppException : Exception
{
    /// <summary>
    /// Código de estado HTTP asociado al error.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Inicializa una nueva instancia de AppException con mensaje y código.
    /// </summary>
    public AppException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}
