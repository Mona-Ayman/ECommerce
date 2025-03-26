using MediatR;

namespace Domain.Products.Events
{
    public class ProductUpdatedEvent : INotification
    {
        #region Constructors

        public ProductUpdatedEvent()
        {
        }

        #endregion
    }
}
