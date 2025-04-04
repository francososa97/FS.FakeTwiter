using MediatR;
using Fs.Framework.Application.DTOs;

public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;
