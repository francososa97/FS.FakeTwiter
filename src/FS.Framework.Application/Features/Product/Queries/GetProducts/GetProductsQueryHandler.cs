using MediatR;
using Fs.Framework.Application.DTOs;
using Fs.Framework.Application.Interfaces;
using FS.FakeTwiter.Application.Interfaces.Tweets;

public class GetProductsQueryHandler(ITweetService service)
    : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
        await service.GetAllAsync();
}
