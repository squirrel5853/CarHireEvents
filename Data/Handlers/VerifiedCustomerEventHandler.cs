using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalService.Data.Events;
using CarRentalService.Data.Services;

namespace CarRentalService.Data.Handlers
{
    public class VerifiedCustomerEventHandler : IEventHandler, IEventHandler<VerifiedCustomerEvent>
    {
        private CustomerService CustomerService { get; }

        public VerifiedCustomerEventHandler(CustomerService customerService)
        {
            CustomerService = customerService;
        }

        public void Handle(VerifiedCustomerEvent integrationEvent)
        {
            using(var unitOfWork = new CommandUnitOfWork(new CommandArgs(integrationEvent.Username, integrationEvent.CorrelationId)))
            { 
                var customer = this.CustomerService.GetCustomer(integrationEvent.Customer.Username).Result;

                customer.SetVerified();

                unitOfWork.RaiseEvent(new LogEvent($"Customer [{customer.Username}] is verified"));

                unitOfWork.Commit();
            }
        }

        public void Handle(IIntegrationEvent integrationEvent)
        {
            Handle((VerifiedCustomerEvent)integrationEvent);
        }
    }
}
