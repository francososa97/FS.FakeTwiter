public class ApiResponse
{
    public bool Success { get; set; } = true;
    public string? Message { get; set; }

    public ApiResponse(string? message = null)
    {
        Message = message ?? "Operación exitosa";
    }
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    public ApiResponse(T data, string? message = null) : base(message)
    {
        Data = data;
    }
    public ApiResponse(bool succes, string message) : base(message)
    {
        Success = succes;
    }
}
