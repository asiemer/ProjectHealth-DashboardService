using System;
using NServiceBus;

namespace Dashboard.Handler
{
    public class PlaceOrder : ICommand
    {
        public Guid Id { get; set; }

        public string Product { get; set; }
    }
}
