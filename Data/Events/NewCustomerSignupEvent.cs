namespace CarRentalService.Data.Events
{
    public class NewCustomerSignupEvent : BaseIntegrationEvent
    {
        public Customer NewCustomer { get; }

        public NewCustomerSignupEvent(Customer customer)
        {
            this.NewCustomer = customer;
        }
    }
}
