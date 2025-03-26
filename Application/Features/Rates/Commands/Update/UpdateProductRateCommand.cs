using MediatR;

namespace Application.Features.Rates.Commands.Update
{
    public record UpdateProductRateCommand(Guid ProductId, decimal Rate) : IRequest
    {
    }
}
