using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ClientFishRESTwebservice
{
    class Program
    {
        static void Main(string[] args)
        {

            RestClient client = new RestClient();
            client.Start();
           

            Console.ReadLine();

        }
    }
}
