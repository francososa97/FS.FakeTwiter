using MediatR;
using Fs.Framework.Application.Interfaces;
using FS.FakeTwiter.Application.Interfaces.Tweets;



public class DeleteProductCommandHandler(ITweetService service)
    : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken) =>
        await service.DeleteAsync(request.Id);
}
