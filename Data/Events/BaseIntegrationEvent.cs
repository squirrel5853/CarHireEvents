namespace CarRentalService.Data.Events
{
    public abstract class BaseIntegrationEvent : IIntegrationEvent
    {
        public string Username { get; set; }
        public string CorrelationId { get; set; }
    }
}
