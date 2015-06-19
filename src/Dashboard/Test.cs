using System;
using System.Security.Cryptography.X509Certificates;
using Dashboard.Infrastructure;
using Dashboard.ReadModel.Views;

namespace Dashboard
{
    public class Test
    {
        public void UpdateRecord(UpdateProjectScore command)
        {
            var _applicationSettings = new ApplicationSettings();
            var writer = new MongoDbProjectionWriter<RAGWidgetView>(_applicationSettings.MongoDbConnectionString, _applicationSettings.MongoDbName);
            var id = new Guid("533e1569-c6f6-4771-acdf-aeeca2ec064c");
            writer.Update(id, x =>
            {
                x.Green = command.Green;
                x.Yellow = command.Yellow;
                x.Red = command.Red;
            });
        }

        public void CreateRecord(UpdateProjectScore command, RAGWidgetView ragWidgetView)
        {
            var _applicationSettings = new ApplicationSettings();
            var writer = new MongoDbProjectionWriter<RAGWidgetView>(_applicationSettings.MongoDbConnectionString, _applicationSettings.MongoDbName);
            var id = new Guid("533e1569-c6f6-4771-acdf-aeeca2ec064c");
            var dashboardView = new RAGWidgetView { Id = id, Yellow = 0, Green = 0, Red = 0 };
            writer.Add(id, dashboardView);
        }
    }
}