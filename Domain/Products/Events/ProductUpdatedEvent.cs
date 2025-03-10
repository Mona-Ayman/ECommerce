using MediatR;

namespace Domain.Products.Events
{
    public class ProductUpdatedEvent : INotification
    {
        #region Constructors

        public ProductUpdatedEvent()
        {
            //Key = key;
        }

        #endregion

        //#region Members

        //public string Key { get; set; }

        //#endregion
    }
}
