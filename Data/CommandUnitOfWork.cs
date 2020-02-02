using CarRentalService.Data.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Data
{
    public class CommandUnitOfWork : IUnitOfWork
    {
        private bool commitSuccessful;

        private string Username { get; }
        private string CorrelationId { get; }

        public CommandUnitOfWork(CommandArgs commandArgs)
        {
            Username = commandArgs.Username;
            CorrelationId = commandArgs.CorrelationId;
        }

        private List<IIntegrationEvent> events = new List<IIntegrationEvent>();

        public void RaiseEvent(IIntegrationEvent integrationEvent)
        {
            integrationEvent.Username = Username;
            integrationEvent.CorrelationId = CorrelationId;
            events.Add(integrationEvent);
        }

        protected void DispatchEvents()
        {
            EventDispatcher.Instance.DispatchEvents(this.events);
        }

        public void Dispose()
        {
            if (commitSuccessful)
            {
                Task.Run(() => this.DispatchEvents());
            }
        }

        internal void Commit()
        {
            commitSuccessful = true;
        }
    }
}
