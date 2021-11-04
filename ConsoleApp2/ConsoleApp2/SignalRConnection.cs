
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class SignalRConnection
    {
        public readonly HubConnection hubConnection;

        public SignalRConnection(string endpoint)
        {
            hubConnection = new HubConnectionBuilder()
             .WithUrl(endpoint)
             //.WithAutomaticReconnect(new TimeSpan[] { new TimeSpan(0,0,0), new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 20), new TimeSpan(0, 0, 30), new TimeSpan(0, 0, 60),
             //new TimeSpan(0,0,90),new TimeSpan(0,0,180)})
             .WithAutomaticReconnect(new RandomRetryPolicy())
             .Build();

            RegisterMethod();
        }

        public Task connect()
        {
            return hubConnection.StartAsync(); 
        }
        public void send()
        {
            hubConnection.SendAsync("JoinRoom","test");
            object s = "111";
            hubConnection.SendCoreAsync("recive", new object[] { s });
            
        }

        public void RegisterMethod()
        {

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine(user + "   " + message);
            });


            hubConnection.On<string>("addStudent", (stu) =>
            {
                Student ss = JsonConvert.DeserializeObject<Student>(stu);
                Console.WriteLine(stu);
            });

            hubConnection.On<string>("groupmethod", value =>
            {
                Console.WriteLine(value);
            });
        }
    }

    public class RandomRetryPolicy : IRetryPolicy
    {
        private readonly Random _random = new Random();

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            // If we've been reconnecting for less than 100 counts so far, reconnect
            if (retryContext.PreviousRetryCount <= 100)
            {
                return TimeSpan.FromSeconds(10);
            }
            else
            {
                // If we've been reconnecting for more than 100 counts, stop reconnecting.
                return null;
            }
        }
    }

    public class Student
    {
        public int id { get; set; }

        public string name { get; set; }

        public Address address { get; set; }
    }

    public class Address
    {
        public int id { get; set; }

        public string location { get; set; }

    }
}
