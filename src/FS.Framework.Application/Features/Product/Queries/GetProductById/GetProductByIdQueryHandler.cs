using MediatR;
using Fs.Framework.Application.DTOs;
using Fs.Framework.Application.Interfaces;
using FS.FakeTwiter.Application.Interfaces.Tweets;

public class GetProductByIdQueryHandler(ITweetService service)
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
        await service.GetByIdAsync(request.Id);
}
