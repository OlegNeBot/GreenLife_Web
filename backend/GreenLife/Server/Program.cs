using GreenLifeLib;
using Nancy.Hosting.Self;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("http://localhost:8080");

            HostConfiguration hostConfig = new HostConfiguration();
            hostConfig.UrlReservations.CreateAutomatically = true;


            using (var host = new NancyHost(uri, new Bootstrapper(), hostConfig))
            {
                host.Start();
                //First connection to database
                using (Context db = new()) { }
                
                Console.WriteLine("Server started!");
                Console.WriteLine("Server is running on " + uri);
                Console.WriteLine("Press any key to close the host.");
                Console.ReadLine();
                Console.WriteLine("Server closed!");
            }
        }
    }
}
