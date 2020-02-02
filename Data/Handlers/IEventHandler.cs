using CarRentalService.Data.Events;
using System.Threading.Tasks;

namespace CarRentalService.Data.Handlers
{
    public interface IEventHandler
    {
        public void Handle(IIntegrationEvent integrationEvent);
    }

    public interface IEventHandler<E> where E : IIntegrationEvent
    {
        public void Handle(E integrationEvent);
    }
}
