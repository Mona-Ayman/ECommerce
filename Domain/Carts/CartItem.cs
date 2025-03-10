using Domain._Base.Models;
using Domain.Products;

namespace Domain.Carts
{
    public class CartItem : BaseEntity<int>
    {
        #region Constructors

        private CartItem()
        {

        }
        public CartItem(Product product)
        {
            ProductId = product.Id;
            Quantity = 1;
            SetPrice(product.Price);
        }

        #endregion

        #region Members

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        #endregion

        #region Methods

        public void IncreaseQuantity()
        {
            Quantity += 1;
            SetPrice(UnitPrice);
        }

        public void DecreaseQuantity()
        {
            Quantity -= 1;
            SetPrice(UnitPrice);
        }

        public void SetPrice(decimal price)
        {
            UnitPrice = price;
        }

        public decimal GetTotalPrice()
        {
            return UnitPrice * Quantity;
        }

        #endregion

    }
}
