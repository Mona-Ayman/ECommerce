using Application.Features.Products.DTO;
using AutoMapper;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQueryHndler : IRequestHandler<GetProductByIdQuery, ProductOutput>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetProductByIdQueryHndler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductOutput> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product = await productRepository.FindByIdAsync(request.Id) ?? throw new NotFoundException(Localizations.NotFound);
            return mapper.Map<ProductOutput>(product);
        }
    }
}
