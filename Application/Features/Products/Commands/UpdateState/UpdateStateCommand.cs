using Domain.Products.Enums;
using MediatR;

namespace Application.Features.Products.Commands.UpdateState
{
    public record UpdateStateCommand(Guid Id, ProductState State) : IRequest;

}
