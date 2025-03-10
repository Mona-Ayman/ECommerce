using Domain.Products.Enums;

namespace Application.Features.Products.DTO
{
    public record ProductOutput(Guid Id, string Name, string Description, decimal Price, ProductState State);

}
