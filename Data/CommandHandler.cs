using CarRentalService.Data.Events;
using CarRentalService.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Data
{
    public class CommandHandler
    {
        private CustomerRegistrationService CustomerRegistrationService;
        private RentalService RentalService;
        private CustomerService CustomerService;
        private CarService CarService;

        public CommandHandler(
            CustomerService customerService,
            CarService carService,
            CustomerRegistrationService customerRegistrationService,
            RentalService rentalService
            )
        {
            CustomerService = customerService;
            CarService = carService;
            CustomerRegistrationService = customerRegistrationService;
            RentalService = rentalService;
        }

        public async Task<IReadOnlyCollection<Customer>> GetCustomers()
        {
            return await this.CustomerService.GetCustomers();
        }

        public async Task<IReadOnlyCollection<Car>> GetCars()
        {
            return await this.CarService.GetCars();
        }

        public async Task SignUp(CommandArgs commandArgs, string username)
        {
            using (var unitOfWork = new CommandUnitOfWork(commandArgs))
            {
                var newCustomer = await this.CustomerRegistrationService.SignUp(username);

                unitOfWork.RaiseEvent(new NewCustomerSignupEvent(newCustomer));

                unitOfWork.RaiseEvent(new LogEvent($"New customer [{newCustomer.Username}] signed up"));

                unitOfWork.Commit();
            }
        }

        public async Task VerifyCustomer(CommandArgs commandArgs, string customerUsername, string verificationCode)
        {
            using (var unitOfWork = new CommandUnitOfWork(commandArgs))
            {
                var customer = await this.CustomerService.GetCustomer(customerUsername);

                if (await this.CustomerRegistrationService.VerifyCode(customer, verificationCode))
                {
                    unitOfWork.RaiseEvent(new VerifiedCustomerEvent(customer));
                }
                else
                {
                    unitOfWork.RaiseEvent(new LogEvent($"Incorrect verificiation code [{verificationCode}] given for customer [{customer.Username}]"));
                }

                unitOfWork.Commit();
            }
        }

        public async Task HireCar(CommandArgs commandArgs, string carname, string customerUsername)
        {
            using (var unitOfWork = new CommandUnitOfWork(commandArgs))
            {
                var customer = await this.CustomerService.GetCustomer(customerUsername);
                var car = await this.CarService.GetCar(carname);

                this.RentalService.Hire(car, customer);

                unitOfWork.RaiseEvent(new LogEvent($"Customer [{customer.Username}] has hired {car.Make} {car.Model}"));

                unitOfWork.Commit();
            }
        }
    }
}
