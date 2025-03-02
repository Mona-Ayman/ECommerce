using MediatR;

namespace Application.Features.Carts.Commands.Update
{
    public record UpdateItemPriceCommand(Guid ProductId, decimal Price) : IRequest
    {
    }
}
