using System;
using NServiceBus;

public class OrderCreatedHandler : IHandleMessages<OrderPlaced>
{
    public void Handle(OrderPlaced message)
    {
        Console.WriteLine(@"Handling: OrderPlaceed for Order Id: {0}", message.OrderId);
    }
}