using System;
using NServiceBus;

namespace Dashboard.Handler
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        IBus bus;

        public PlaceOrderHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(PlaceOrder message)
        {
            Console.WriteLine(@"Order for Product:{0} placed with id: {1}", message.Product, message.Id);

            Console.WriteLine(@"Publishing: OrderPlaced for Order Id: {0}", message.Id);

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.Id
            };
            bus.Publish(orderPlaced);
        }
    }
}

