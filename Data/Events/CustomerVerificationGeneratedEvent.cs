namespace CarRentalService.Data.Events
{
    public class CustomerVerificationGeneratedEvent : BaseIntegrationEvent
    {
        public string CustomerName { get; set; }
        public string VerificationCode { get; set; }

        public CustomerVerificationGeneratedEvent(string username, string verificationCode)
        {
            CustomerName = username;
            VerificationCode = verificationCode;
        }
    }
}