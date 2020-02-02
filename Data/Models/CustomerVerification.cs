namespace CarRentalService.Data
{
    public class CustomerVerification
    {
        public CustomerVerification(string username, string verificationCode)
        {
            Username = username;
            VerificationCode = verificationCode;
        }

        public string Username { get; }
        public string VerificationCode { get; }
    }
}