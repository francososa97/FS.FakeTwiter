using MediatR;
using Fs.Framework.Application.DTOs;
using Fs.Framework.Application.Interfaces;
using FS.FakeTwiter.Application.Interfaces.Tweets;



public class UpdateProductCommandHandler(ITweetService service)
    : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var dto = new ProductDto
        {
            Id = request.Id,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio,
            Stock = request.Stock
        };

        await service.UpdateAsync(request.Id, dto);
    }
}
