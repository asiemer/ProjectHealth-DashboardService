using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dashboard.Infrastructure;
using Dashboard.ReadModel.Views;
using MongoDB.Driver;

namespace Dashboard.ReadModel.Providers
{
    public interface IDashboardProvider : IProvider<RAGWidgetView>
    {
        Task<RAGWidgetView> Get(Guid id);
    }

    public class RAGWidgetViewProvider : MongoDbProvider<RAGWidgetView>, IDashboardProvider
    {
        public RAGWidgetViewProvider(IApplicationSettings applicationSettings) : base(applicationSettings)
        {}

        public async Task<RAGWidgetView> Get(Guid id)
        {
            var collection = GetCollection();
            var items = await collection.Find(x => x.Id == id).ToListAsync();
            return items.First();
        }
    }
}