using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using GrpcServer.Protos;
using System;

namespace GrpcClient
{

    class Program
    {
        static async Task  Main(string[] args)
        {
            //var input = new HelloRequest{ Name = "Uno" };
            //var channel = GrpcChannel.ForAddress("https://localhost:7296");
            //var client = new Greeter.GreeterClient(channel);

            //var reply =  client.SayHello(input);

            //Console.WriteLine(reply);

            var channel = GrpcChannel.ForAddress("https://localhost:7296");
            var customerClient = new Customer.CustomerClient(channel);

            var clientRequested = new CustomerLookupModel { UserId = 1 };

            var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

            Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            Console.WriteLine();
            Console.WriteLine("new Customer list");
            using(var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {

                while(await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} : { currentCustomer.EmailAddress}");

                }


            }




            Console.ReadLine();


        }
    }




}