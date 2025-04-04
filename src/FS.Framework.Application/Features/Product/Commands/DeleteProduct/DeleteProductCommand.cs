using MediatR;

public record DeleteProductCommand(int Id) : IRequest;