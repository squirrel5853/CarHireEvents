namespace CarRentalService
{
    public class Authentication
    {
        public static string LoggedInUser { get; set; }

        public void Authorize(string username)
        {
            LoggedInUser = username;
        }
    }
}
