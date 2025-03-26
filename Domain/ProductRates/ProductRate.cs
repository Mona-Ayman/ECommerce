using Domain._Base.Models;
using Domain.ProductRates.Events;
using Shared.Exceptions;
using Shared.Resources;

namespace Domain.ProductRates
{
    public class ProductRate : BaseEntity<int>
    {
        #region Constructors

        private ProductRate() { }
        public ProductRate(Guid productId, string userId, decimal rate)
        {
            CheckRateRange(rate);

            ProductId = productId;
            UserId = userId;
            Rate = rate;

            AddPProductRatedEvent(ProductId, null, rate);
        }

        #endregion

        #region Memebers

        public Guid ProductId { get; private set; }
        //public Product Product { get; private set; }
        public string UserId { get; private set; }
        //public User User { get; private set; }
        public decimal Rate { get; private set; }

        #endregion

        #region Actions

        public void Update(decimal newRate)
        {
            CheckRateRange(newRate);
            AddPProductRatedEvent(ProductId, Rate, newRate);

            Rate = newRate;
        }

        #endregion

        #region Private Methods

        public void CheckRateRange(decimal rate)
        {
            if (rate < 1 || rate > 5)
                throw new ValidationException(Localizations.RateRange);
        }

        private void AddPProductRatedEvent(Guid productId, decimal? oldRate, decimal newRate)
        {
            ProductRatedEvent productRatedEvent = new(productId, oldRate, newRate);
            AddDomainEvent(productRatedEvent);
        }

        #endregion

    }
}
