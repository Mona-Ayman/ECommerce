using Application.Extensions;
using Domain._Base.Interfaces;
using Domain.ProductRates;
using Domain.ProductRates.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Rates.Commands.Create
{
    public class CreateProductRateCommandHandler : IRequestHandler<CreateProductRateCommand>
    {
        #region Fields

        private readonly IProductRateRepository productRateRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        #endregion

        #region Constructors

        public CreateProductRateCommandHandler(IProductRateRepository productRateRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.productRateRepository = productRateRepository;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        public async Task Handle(CreateProductRateCommand request, CancellationToken cancellationToken)
        {
            string userId = httpContextAccessor.GetUserId();
            ProductRate rate = new(request.ProductId, userId, request.Rate);

            productRateRepository.Add(rate);
            await unitOfWork.SaveAsync(cancellationToken);
        }

        #endregion
    }
}
