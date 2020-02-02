using CarRentalService.Data.Events;

namespace CarRentalService.Data
{
    public class LogEvent : BaseIntegrationEvent
    {
        public string Message { get; set; }

        public LogEvent(string message)
        {
            Message = message;
        }
    }
}