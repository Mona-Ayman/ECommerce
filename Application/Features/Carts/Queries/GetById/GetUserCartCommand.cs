using Application.Features.Carts.Outputs;
using MediatR;

namespace Application.Features.Carts.Queries.GetById
{
    public class GetUserCartCommand : IRequest<CartOutput>
    {
    }
}
