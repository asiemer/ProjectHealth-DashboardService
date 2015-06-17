using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Dashboard.ReadModel.Providers;
using Dashboard.ReadModel.Views;

namespace Dashboard.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        private readonly IDashboardProvider _dashboardProvider;

        public DashboardController(IDashboardProvider dashboardProvider)
        {
            _dashboardProvider = dashboardProvider;
        }

        [HttpGet]
        [Route("getRAGWidget/{RAGWidgetId}")]
        public async Task<RAGWidgetInfo> GetRAGWidgetInformation(Guid ragWidgetId)
        {
            var ragWidgetFromDB = await _dashboardProvider.Get(ragWidgetId);
            return ragWidgetFromDB.ToRagWidgetInfo();
        }
    }

    public static class ExtendsRAGWidgetInfo
    {
        public static RAGWidgetInfo ToRagWidgetInfo(this RAGWidgetView x)
        {
            return new RAGWidgetInfo
            {
                item = new List<TextValue>
                {
                    new TextValue {text = "Red", value = x.RedValue},
                    new TextValue {text = "Yellow", value = x.AmberValue},
                    new TextValue {text = "Green", value = x.GreenValue},
                }
            };
        }
    }

    public class RAGWidgetInfo
    {
        public List<TextValue> item { get; set; }
    }

    public class TextValue
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class CreateRAGWidgetRequest
    {
        public string Name { get; set; }
    }

    public class RAGColumnModel
    {
        public string RedString { get {return "Red"; }}
        public int RedValue { get; set; }
        public string AmberString { get { return "Amber"; } }
        public int AmberValue { get; set; }
        public string GreenString { get { return "Green"; } }
        public int GreenValue { get; set; }
    }
}