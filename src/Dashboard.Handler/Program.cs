using System;
using NServiceBus;

class Program
{
    static void Main()
    {
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Dashboard.Dashboard.Handler");
        busConfiguration.UseSerialization<XmlSerializer>();
        busConfiguration.EnableInstallers();
        busConfiguration.UsePersistence<InMemoryPersistence>();

        using (IBus bus = Bus.Create(busConfiguration).Start())
        {
            Console.WriteLine("To exit press 'Ctrl + C'");
            Console.ReadLine();
        }
    }
}