using System;
using NServiceBus;

namespace Dashboard.Handler
{
    public class Program
    {

        static void Main()
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Dashboard.Dashboard.Handler");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (IBus bus = Bus.Create(busConfiguration).Start())
            {
                Console.WriteLine("To exit press 'Ctrl + C'");
                Console.ReadLine();
            }
        }
    }
}
