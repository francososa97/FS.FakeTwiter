using MediatR;

public record UpdateProductCommand(int Id, string Nombre, string Descripcion, decimal Precio, int Stock)
    : IRequest;