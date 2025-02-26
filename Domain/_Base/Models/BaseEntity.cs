namespace Domain._Base.Models
{
    public abstract class BaseEntity<TKey> : DomainEvents
    {
        #region Members

        public TKey Id { get; set; }

        #endregion
    }
}
