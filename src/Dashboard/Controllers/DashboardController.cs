using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Dashboard.Extentions;
using Dashboard.Infrastructure;
using Dashboard.Models.RAGWidgetModels;
using Dashboard.ReadModel.Providers;
using Dashboard.ReadModel.Views;

namespace Dashboard.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        private readonly IRAGWidgetViewProvider _ragWidgetViewProvider;

        public DashboardController(IRAGWidgetViewProvider ragWidgetViewProvider)
        {
            _ragWidgetViewProvider = ragWidgetViewProvider;
        }

        [HttpGet]
        [Route("getRAGWidget/{RAGWidgetId}")]
        public async Task<RAGWidgetInfo> GetRAGWidgetInformation(Guid ragWidgetId)
        {
            
            var ragWidgetFromDb = await _ragWidgetViewProvider.Get(ragWidgetId);
            return ragWidgetFromDb.ToRagWidgetInfo();
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateRAGWidgetInformation(CreateRAGWidgetRequest req)
        {
            //This is just here temporary until we figure out how we want to handle incoming events.
            //var _applicationSettings = new ApplicationSettings();
            //var writer = new MongoDbProjectionWriter<RAGWidgetView>(_applicationSettings.MongoDbConnectionString, _applicationSettings.MongoDbName);
            //var id = new Guid("533e1569-c6f6-4771-acdf-aeeca2ec064c");
            //var dashboardView = new RAGWidgetView { Id = id, Yellow = 0, Green = 0, Red = 0 };
            //writer.Add(id, dashboardView);
            //new Test().CreateRecord();

            return Request.CreateResponse(HttpStatusCode.Created, "RAG Information Created in Dashboard DB");
        }
    }
}