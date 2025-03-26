using MediatR;

namespace Application.Features.Carts.Commands.Create
{
    public record AddItemCommand(Guid ProductId) : IRequest
    {
    }
}
