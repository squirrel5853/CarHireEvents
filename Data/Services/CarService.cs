using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Data.Services
{
    public class CarService
    {
        public async Task<IReadOnlyCollection<Car>> GetCars()
        {
            return await Task.FromResult(Context.CurrentContext.Cars);
        }

        internal async Task<Car> GetCar(string carname)
        {
            return await Task.FromResult(Context.CurrentContext.Cars.FirstOrDefault(x => x.Make == carname));
        }
    }
}
