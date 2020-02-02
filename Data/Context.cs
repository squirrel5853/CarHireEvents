using System;
using System.Collections.Generic;

namespace CarRentalService.Data
{
    public class Context
    {
        private static object _padlock = new object();

        public static Context CurrentContext {
            get
            {
                if (_context == null)
                {
                    lock (_padlock)
                    {
                        if (_context == null)
                        {
                            _context = new Context();

                            _context.AddCar(new Car("Ford", "Mustang"));
                            _context.AddCar(new Car("Porsche", "911 Carrera 4S"));
                            _context.AddCar(new Car("Nissan", "GTR"));
                            _context.AddCar(new Car("Bentley", "GTC CAB"));
                            _context.AddCar(new Car("Ferrari", "California"));
                            _context.AddCar(new Car("Lamborghini", "Huracan Evo"));
                        }
                    }
                }

                return _context;
            }
        }

        internal void AddCustomerVerification(CustomerVerification customerVerification)
        {
            _verificationCodes.Add(customerVerification);
        }

        private void AddCar(Car car)
        {
            _cars.Add(car);
        }

        private static Context _context;

        internal void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }


        public IReadOnlyCollection<Customer> Customers { get { return _customers; } }
        private List<Customer> _customers = new List<Customer>();

        public IReadOnlyCollection<Car> Cars { get { return _cars; } }
        private List<Car> _cars = new List<Car>();

        public IReadOnlyCollection<CarRental> CarRentals { get { return _carRentals; } }
        private List<CarRental> _carRentals = new List<CarRental>();

        public IReadOnlyCollection<CustomerVerification> VerificationCodes { get { return _verificationCodes; } }
        private List<CustomerVerification> _verificationCodes = new List<CustomerVerification>();
    }
}
