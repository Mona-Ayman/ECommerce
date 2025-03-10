using Domain._Base.Models;
using Domain.Products.Enums;
using Domain.Products.Events;
using Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Domain.Products
{
    public class Product : BaseEntity<Guid>
    {
        #region Constructors

        private Product() { }

        public Product(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException(Localizations.RequiredName);

            if (string.IsNullOrWhiteSpace(description))
                throw new ValidationException(Localizations.RequiredDescription);

            if (price <= 0)
                throw new ValidationException(Localizations.PriceRange);

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            State = ProductState.Active;

            LogPriceChanges(price);
        }

        #endregion

        #region Memebers

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool IsDeleted { get; private set; }
        public ProductState State { get; private set; }
        public ICollection<TrackingProductPrice> TrackingPrices { get; private set; } = new List<TrackingProductPrice>();

        #endregion

        #region Actions
        public void Update(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException(Localizations.RequiredName);

            if (string.IsNullOrWhiteSpace(description))
                throw new ValidationException(Localizations.RequiredDescription);

            if (price <= 0)
                throw new ValidationException(Localizations.PriceRange);

            Name = name;
            Description = description;
            Price = price;

            LogPriceChanges(price);
            AddProductUpdatedEvent();
        }

        public void Delete()
        {
            IsDeleted = true;
            AddProductUpdatedEvent();
        }

        public void ChangeState(ProductState state)
        {
            State = state;
            AddProductUpdatedEvent();
        }

        #endregion

        #region Private Methods

        private void LogPriceChanges(decimal price)
        {
            TrackingProductPrice lastPrice = this.TrackingPrices.LastOrDefault();
            if (lastPrice?.Price == price)
                return;

            TrackingPrices.Add(new TrackingProductPrice(price));

            ProductPriceChangedEvent productPriceChangedEvent = new(this.Id, price);
            AddDomainEvent(productPriceChangedEvent);
        }

        private void AddProductUpdatedEvent()
        {
            ProductUpdatedEvent productUpdatedEvent = new();
            AddDomainEvent(productUpdatedEvent);
        }

        #endregion
    }
}
