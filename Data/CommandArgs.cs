using CarRentalService.Data.Events;

namespace CarRentalService.Data
{
    public class CommandArgs
    {
        public CommandArgs(string username, string correlationId)
        {
            this.Username = username;
            this.CorrelationId = correlationId;
        }

        public string Username { get; internal set; }
        public string CorrelationId { get; internal set; }

        public static CommandArgs FromIntegrationEvent(IIntegrationEvent integrationEvent)
        {
            return new CommandArgs(integrationEvent.Username, integrationEvent.CorrelationId);
        }
    }
}