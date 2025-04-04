using MediatR;
using Fs.Framework.Application.DTOs;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
