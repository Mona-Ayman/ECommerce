using MediatR;

namespace Domain._Base.Models
{
    public class DomainEvents
    {
        #region Members

        private List<INotification> domainEvents;
        public IReadOnlyCollection<INotification> Events => domainEvents?.AsReadOnly();

        #endregion

        #region Methods

        public void AddDomainEvent(INotification eventItem)
        {
            domainEvents = domainEvents ?? new List<INotification>();
            domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }

        #endregion
    }
}
