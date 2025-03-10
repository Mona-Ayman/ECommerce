using MediatR;

namespace Domain.Products.Events
{
    public class ProductPriceChangedEvent : INotification
    {
        #region Constructors

        private ProductPriceChangedEvent()
        {
        }

        public ProductPriceChangedEvent(Guid productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }

        #endregion

        #region Members

        public Guid ProductId { get; private set; }
        public decimal Price { get; private set; }

        #endregion
    }
}
