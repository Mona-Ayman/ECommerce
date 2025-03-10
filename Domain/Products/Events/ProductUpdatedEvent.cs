using MediatR;

namespace Domain.Products.Events
{
    public class ProductUpdatedEvent : INotification
    {
        #region Constructors

        private ProductUpdatedEvent()
        {
        }

        public ProductUpdatedEvent(string key)
        {
            Key = key;
        }

        #endregion

        #region Members

        public string Key { get; set; }

        #endregion
    }
}
