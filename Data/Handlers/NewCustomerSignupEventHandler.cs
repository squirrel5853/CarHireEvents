using CarRentalService.Data.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace CarRentalService.Data.Handlers
{
    public class NewCustomerSignupEventHandler : IEventHandler, IEventHandler<NewCustomerSignupEvent>
    {
        Random random = new Random(DateTime.UtcNow.Second);

        public void Handle(NewCustomerSignupEvent integrationEvent)
        {
            using (var unitOfWork = new CommandUnitOfWork(CommandArgs.FromIntegrationEvent(integrationEvent)))
            {
                Thread.Sleep(3000);

                string verificationCode = this.GenerateCustomerCode(integrationEvent.NewCustomer);

                unitOfWork.RaiseEvent(new CustomerVerificationGeneratedEvent(integrationEvent.NewCustomer.Username, verificationCode));

                unitOfWork.RaiseEvent(new LogEvent($"Customer [{integrationEvent.NewCustomer.Username}] verification code generated [{verificationCode}]"));

                CustomerVerification customerVerification = new CustomerVerification(integrationEvent.NewCustomer.Username, verificationCode);
                Context.CurrentContext.AddCustomerVerification(customerVerification);

                unitOfWork.Commit();
            }
        }

        public void Handle(IIntegrationEvent integrationEvent)
        {
            Handle((NewCustomerSignupEvent)integrationEvent);
        }

        private string GenerateCustomerCode(Customer newCustomer)
        {
            StringBuilder sbCode = new StringBuilder();

            while (sbCode.Length < 9)
            {
                int randomNumber = random.Next(65, 89);

                sbCode.Append(((char)randomNumber).ToString());
            }

            int firstNumber = (int)newCustomer.Username.First();
            int secondNumber = (int)newCustomer.Username.Last();

            return $"{firstNumber}{sbCode.ToString()}{secondNumber}";
        }
    }
}
