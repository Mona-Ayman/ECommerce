using MediatR;

namespace Application.Features.Rates.Commands.Create
{
    public record CreateProductRateCommand(Guid ProductId, decimal Rate) : IRequest
    {
    }
}
