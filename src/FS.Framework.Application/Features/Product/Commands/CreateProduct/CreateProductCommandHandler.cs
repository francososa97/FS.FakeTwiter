using MediatR;
using Fs.Framework.Application.DTOs;
using Fs.Framework.Application.Interfaces;
using FS.Framework.Domain.Entities;
using FS.FakeTwiter.Application.Interfaces.Tweets;


public class CreateProductCommandHandler(ITweetService service)
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var dto = new ProductDto
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio,
            Stock = request.Stock
        };

        return await service.CreateAsync(dto);
    }
}
