using Domain._Base.Models;

namespace Domain.Products
{
    public class TrackingProductPrice : BaseEntity<int>
    {
        #region Constructors

        private TrackingProductPrice()
        {
        }

        public TrackingProductPrice(decimal price)
        {
            CreatedAt = DateTime.UtcNow;
            Price = price;
        }

        #endregion

        #region Members

        public DateTime CreatedAt { get; private set; }

        public decimal Price { get; private set; }

        #endregion
    }
}
