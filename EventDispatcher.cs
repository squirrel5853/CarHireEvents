using CarRentalService.Data.Events;
using CarRentalService.Data.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CarRentalService
{
    public class EventDispatcher
    {
        public static EventDispatcher Instance { get { return _instance; } }

        public IServiceProvider ServiceProvider { get; private set; }

        private static EventDispatcher _instance = new EventDispatcher();

        internal void Configure(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void DispatchEvents<E>(IEnumerable<E> integrationEvents) where E : IIntegrationEvent
        {
            foreach (var integrationEvent in integrationEvents)
            {
                var eventHandlers = ServiceProvider.GetServices<IEventHandler>();

                eventHandlers = eventHandlers
                                    .Where(handler => ((TypeInfo)handler.GetType()).ImplementedInterfaces
                                        .Any(i => i.GenericTypeArguments.FirstOrDefault() == integrationEvent.GetType()))
                                    .ToList();

                foreach (var eventHandler in eventHandlers)
                {
                    eventHandler.Handle(integrationEvent);
                }
            }
        }

        
    }
}
