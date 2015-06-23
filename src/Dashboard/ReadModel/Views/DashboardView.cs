using System;

namespace Dashboard.ReadModel.Views
{
    public class DashboardView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class RagWidgetView
    {
        public int Red { get; set; }
        public int Yellow { get; set; }
        public int Green { get; set; }
        public Guid Id { get; set; }
    }
}