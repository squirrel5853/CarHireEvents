using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Data.Services
{
    public class CustomerRegistrationService
    {
        public async Task<Customer> SignUp(string username)
        {
            var customer = new Customer(username);
            Context.CurrentContext.AddCustomer(customer);

            return await Task.FromResult(customer);
        }

        internal async Task<bool> VerifyCode(Customer customer, string verificationCode)
        {
            var customerVerification = Context.CurrentContext.VerificationCodes.FirstOrDefault(x => x.Username == customer.Username);

            return await Task.FromResult(customerVerification?.VerificationCode == verificationCode);
        }
    }
}
