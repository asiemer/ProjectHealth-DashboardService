using System;

namespace Dashboard.ReadModel.Views
{
    public class DashboardView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class RAGWidgetView
    {
        public int RedValue { get; set; }
        public int AmberValue { get; set; }
        public int GreenValue { get; set; }
        public Guid Id { get; set; }
    }
}