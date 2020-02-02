namespace CarRentalService.Data
{
    public class Customer
    {
        public Customer(string username)
        {
            Username = username;
            IsVerified = false;
        }

        public string Username { get; set; }

        public bool IsVerified { get; private set; }

        public void SetVerified()
        {
            IsVerified = true;
        }
    }
}