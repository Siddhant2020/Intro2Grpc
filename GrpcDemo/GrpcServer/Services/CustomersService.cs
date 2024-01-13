using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookUpModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Siddhant";
                output.LastName = "Ashok";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Siddhi";
                output.LastName = "Ashok";
            }
            else
            {
                output.FirstName = "Tim";
                output.LastName = "Corey";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Jermy",
                    LastName = "Renner",
                    EmailAddress = "jeremy.com",
                    Age = 41,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Scarlett",
                    LastName = "Johansonn",
                    EmailAddress = "scarlett.com",
                    Age = 37,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Robert",
                    LastName = "Downey Jr.",
                    EmailAddress = "robert.com",
                    Age = 47,
                    IsAlive = true
                }
                
            };

            foreach (var cust in customers)
            {
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
