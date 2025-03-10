using Application.Features.Products.DTO;
using Domain._Base.Models;
using MediatR;

namespace Application.Features.Products.Queries.GetAll
{
    public record GetAllProductsQuery(string Search, int? MinPrice, int? MaxPrice, int PageSize, int PageNumber) : IRequest<PaginatedModel<ProductOutput>>;
}
