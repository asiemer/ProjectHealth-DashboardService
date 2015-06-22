using System;
using System.Threading.Tasks;
using Dashboard.Infrastructure;
using Dashboard.ReadModel.Providers;
using Dashboard.ReadModel.Views;

namespace Dashboard
{
    public class MongoDbModifier
    {
        private readonly IRAGWidgetViewProvider _ragWidgetViewProvider;
        private readonly MongoDbProjectionWriter<RAGWidgetView> _writer; 

        public MongoDbModifier(IRAGWidgetViewProvider ragWidgetViewProvider)
        {
            _ragWidgetViewProvider = ragWidgetViewProvider;
            IApplicationSettings applicationSettings = new ApplicationSettings();
            _writer = new MongoDbProjectionWriter<RAGWidgetView>(applicationSettings.MongoDbConnectionString, applicationSettings.MongoDbName);
        }

        public async void UpdateDatabase(UpdateProjectScore command)
        {
            var dbitem = await CheckIfRAGWidgetExists(command.Id);
            var exists = (dbitem != null);


            if (exists)
            {
                UpdateRecord(command);
            }
            else
            {
                CreateRecord(command);
            }
        }

        private async void UpdateRecord(UpdateProjectScore command)
        {
            await _writer.Update(command.Id, x =>
            {
                x.Green = command.Green;
                x.Yellow = command.Yellow;
                x.Red = command.Red;
            });
        }

        private async void CreateRecord(UpdateProjectScore command)
        {
            var ragWidgetView = new RAGWidgetView 
            { 
                Id = command.Id,
                Yellow = command.Yellow,
                Green = command.Green,
                Red = command.Red
            };

            await _writer.Add(command.Id, ragWidgetView);
        }

        private async Task<RAGWidgetView> CheckIfRAGWidgetExists(Guid id)
        {
            RAGWidgetView dbItem = await _ragWidgetViewProvider.GetById(id);
            return dbItem;
        }
    }
}