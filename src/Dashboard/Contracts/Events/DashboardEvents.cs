using System;

namespace Dashboard.Contracts.Events
{
    public class RAGWidgetCreated
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}