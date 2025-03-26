using MediatR;

namespace Domain.ProductRates.Events
{
    public class ProductRatedEvent : INotification
    {
        #region Constructors

        public ProductRatedEvent(Guid productId, decimal? oldRate, decimal newRate)
        {
            ProductId = productId;
            OldRate = oldRate;
            NewRate = newRate;
        }

        #endregion

        #region Members

        public Guid ProductId { get; private set; }
        public decimal? OldRate { get; private set; }
        public decimal NewRate { get; private set; }

        #endregion
    }
}
