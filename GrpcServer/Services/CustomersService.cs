using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServer.Services
{
    public class CustomersService:Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> logger;

        public CustomersService(ILogger<CustomersService>logger)
        {
            this.logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {

                output.FirstName = "Jamie";
                output.LastName = "Smith";

            }
            else if(request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else
            {
                output.FirstName = "Greg";
                output.LastName = "Thomas";
            }

            return Task.FromResult(output);




        }



        public override async Task GetNewCustomers(NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "oo",
                    LastName = "oo",
                    EmailAddress = "oooo@gmail.com",
                    Age =40,
                    IsAlive = true,
                },

                new CustomerModel
                {
                    FirstName = "bb",
                    LastName = "bbbb",
                    EmailAddress = "bb@gmail.com",
                    Age =40,
                    IsAlive = false,
                },
                new CustomerModel
                {
                    FirstName = "aa",
                    LastName = "aa",
                    EmailAddress = "aaaa@gmail.com",
                    Age =40,
                    IsAlive = true,
                },
                new CustomerModel
                {
                    FirstName = "cc",
                    LastName = "cc",
                    EmailAddress = "cccc@gmail.com",
                    Age =40,
                    IsAlive = true,
                }


            };

            foreach(var cust in customers)
            {

                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
                


            }

        }

    }
}
