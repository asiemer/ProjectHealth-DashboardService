using System;
using System.Threading.Tasks;
using System.Web.Http;
using Dashboard.Extentions;
using Dashboard.Models.RAGWidgetModels;
using Dashboard.ReadModel.Providers;

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
    }
}