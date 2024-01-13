using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "Siddhant" };
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);
            //Console.WriteLine(reply.Message);

            var input = new CustomerLookUpModel { UserId = 1 };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Customer.CustomerClient(channel);

            var reply = await client.GetCustomerInfoAsync(input);
            Console.WriteLine($"{reply.FirstName} {reply.LastName}");
            Console.WriteLine();
            Console.WriteLine("New Customer List:");
            Console.WriteLine();

            using (var call = client.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;

                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} : {currentCustomer.EmailAddress}");
                }
            }

            Console.ReadLine();
        }
    }
}
