using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalService.Data.Services
{
    public class RentalService
    {
        public void Hire(Car car, Customer customer)
        {
            if (!customer.IsVerified)
            {

                return;
            }
        }
    }
}
