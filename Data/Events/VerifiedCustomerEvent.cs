using CarRentalService.Data.Events;

namespace CarRentalService.Data
{
    public class VerifiedCustomerEvent : BaseIntegrationEvent
    {
        public Customer Customer { get; set; }

        public VerifiedCustomerEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}