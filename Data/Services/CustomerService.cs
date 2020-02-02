using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Data.Services
{
    public class CustomerService
    {
        public async Task<IReadOnlyCollection<Customer>> GetCustomers()
        {
            return await Task.FromResult(Context.CurrentContext.Customers);
        }

        internal async Task<Customer> GetCustomer(string customerUsername)
        {
            return await Task.FromResult(Context.CurrentContext.Customers.FirstOrDefault(x => x.Username == customerUsername));
        }
    }
}
