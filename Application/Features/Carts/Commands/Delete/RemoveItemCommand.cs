using MediatR;

namespace Application.Features.Carts.Commands.Delete
{
    public record RemoveItemCommand(Guid ProductId) : IRequest;
}
