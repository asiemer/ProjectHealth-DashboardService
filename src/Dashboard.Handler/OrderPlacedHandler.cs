using System;
using NServiceBus;

public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
{
    IBus bus;

    public OrderPlacedHandler(IBus bus)
    {
        this.bus = bus;
    }

    public void Handle(OrderPlaced message)
    {
        Console.WriteLine(@"Order for Product:{0} placed with id: {1}", message.Product, message.Id);

        Console.WriteLine(@"Publishing: OrderPlaced for Order Id: {0}", message.Id);
    }
}