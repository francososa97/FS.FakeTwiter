using Fs.Framework.Application.DTOs;
using MediatR;

public record CreateProductCommand(string Nombre, string Descripcion, decimal Precio, int Stock)
    : IRequest<ProductDto>;
