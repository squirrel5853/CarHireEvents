namespace CarRentalService.Data.Events
{
    public interface IIntegrationEvent
    {
        string Username { get; set; }
        string CorrelationId { get; set; }
    }
}
