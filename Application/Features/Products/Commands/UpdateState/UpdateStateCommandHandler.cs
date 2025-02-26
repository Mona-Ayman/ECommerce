using Domain._Base.Interfaces;
using Domain.Products;
using Domain.Products.Interfaces;
using MediatR;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Products.Commands.UpdateState
{
    public class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand>
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateStateCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            Product product = await productRepository.FindByIdAsync(request.Id);
            if (product == null) throw new NotFoundException(Localizations.NotFound);

            product.ChangeState(request.State);
            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
