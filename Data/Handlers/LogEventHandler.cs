using System;
using CarRentalService.Data.Events;

namespace CarRentalService.Data.Handlers
{
    public class LogEventHandler : IEventHandler, IEventHandler<LogEvent>
    {
        public void Handle(LogEvent integrationEvent)
        {
            Console.WriteLine(integrationEvent.Message);
        }

        public void Handle(IIntegrationEvent integrationEvent)
        {
            Handle((LogEvent)integrationEvent);
        }
    }
}
