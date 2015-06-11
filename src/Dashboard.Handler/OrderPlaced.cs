using System;
using NServiceBus;

namespace Dashboard.Handler
{
    public class OrderPlaced : IEvent
    {
        public Guid OrderId { get; set; }
    }
}

