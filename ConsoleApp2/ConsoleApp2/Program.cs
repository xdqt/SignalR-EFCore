using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {

        public static SignalRConnection connection = new SignalRConnection("http://localhost:7777/chatHub");
        static  void Main(string[] args)
        {
            Task t= connection.connect();
            t.Wait();
            connection.send();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
