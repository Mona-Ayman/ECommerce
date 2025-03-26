using Application.Extensions;
using Domain._Base.Interfaces;
using Domain.ProductRates;
using Domain.ProductRates.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Exceptions;
using Shared.Resources;

namespace Application.Features.Rates.Commands.Update
{
    public class UpdateProductRateCommandHandler : IRequestHandler<UpdateProductRateCommand>
    {

        #region Fields

        private readonly IProductRateRepository productRateRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        #endregion

        #region Constructors

        public UpdateProductRateCommandHandler(IProductRateRepository productRateRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.productRateRepository = productRateRepository;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        public async Task Handle(UpdateProductRateCommand request, CancellationToken cancellationToken)
        {
            string userId = httpContextAccessor.GetUserId();

            ProductRate rate = await productRateRepository.GetByProductIdAndUserIdAsync(userId, request.ProductId) ?? throw new NotFoundException(Localizations.NotFound);
            rate.Update(request.Rate);

            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion

    }
}
