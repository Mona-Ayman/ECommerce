using Application.Features.Products.DTO;
using MediatR;

namespace Application.Features.Products.Queries.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductOutput>;
}
