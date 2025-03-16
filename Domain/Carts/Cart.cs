using Domain._Base.Models;
using Domain.Products;
using Domain.Users;

namespace Domain.Carts
{
    public class Cart : BaseEntity<int>
    {
        #region Constructors

        private Cart()
        {
        }


        public Cart(string userId)
        {
            UserId = userId;
        }

        #endregion

        #region Members

        public string UserId { get; private set; }
        public User User { get; private set; }
        public ICollection<CartItem> Items { get; private set; } = new List<CartItem>();

        #endregion

        #region Methods

        public void AddItem(Product product)
        {
            CartItem cartItem = Items.FirstOrDefault(i => i.ProductId == product.Id);

            if (cartItem == null)
                Items.Add(new CartItem(product));
            else
                cartItem.IncreaseQuantity();
        }

        public void RemoveItem(Product product)
        {
            CartItem cartItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
            if (cartItem == null)
                return;

            if (cartItem.Quantity <= 1)
                Items.Remove(cartItem);
            else
                cartItem.DecreaseQuantity();

        }

        #region Using BackgroundService Approach

        public void ChangeItemPrice(Guid productId, decimal price)
        {

            List<CartItem> cartItems = Items.Where(i => i.ProductId == productId).ToList();
            if (cartItems == null) return;

            foreach (var item in cartItems)
                item.SetPrice(price);
        }

        #endregion

        #endregion
    }
}
