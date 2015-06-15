using System;
using NServiceBus;

public class OrderPlaced : IEvent
{
    public Guid Id { get; set; }

    public string Product { get; set; }
}