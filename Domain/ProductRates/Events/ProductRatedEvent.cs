using MediatR;

namespace Domain.ProductRates.Events
{
    public class ProductRatedEvent : INotification
    {
        #region Constructors

        public ProductRatedEvent(Guid productId)
        {
            ProductId = productId;
        }

        #endregion

        #region Members

        public Guid ProductId { get; private set; }

        #endregion
    }
}
