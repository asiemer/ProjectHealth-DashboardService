using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashboard.Infrastructure;
using Dashboard.ReadModel.Views;
using Newtonsoft.Json.Linq;

namespace Dashboard.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        //private readonly IDashboardApplicationService _dashboardApplicationService;
        private readonly IApplicationSettings _applicationSettings;

        public DashboardController(IApplicationSettings applicationSettings)
        {
            //_dashboardApplicationService = dashboardApplicationService;
            _applicationSettings = applicationSettings;
        }

        [HttpGet]
        [Route("getRAGWidget")]
        public JToken GetRAGWidgetInformation()
        {
            var jsonToParse = CreateRAGJsonString();
            JToken json = JObject.Parse(jsonToParse);
            return json;
        }

        private string CreateRAGJsonString()
        {
            var jsonToParse = "";
            var beginningJson = "{'item': [";
            var ragModel = new RAGColumnModel
            {
                RedValue = 10,
                AmberValue = 15,
                GreenValue = 20,
            };
            var first = "{'value':" + ragModel.RedValue + ",'text':'" + ragModel.RedString + "'},";
            var second = jsonToParse + "{'value':" + ragModel.AmberValue + ",'text':'" + ragModel.AmberString + "'},";
            var third = jsonToParse + "{'value':" + ragModel.GreenValue + ",'text':'" + ragModel.GreenString + "'},";
            var end = "]}";
            var totalJson = beginningJson + first + second + third + end;
            return totalJson;
        }

        //CreateRAGWidgetInformation will be temporary until we have information to set from events
        //From the ProjectsService
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateRAGWidgetInformation(CreateRAGWidgetRequest req)
        {
            //This is just here temporary until we figure out how we want to handle incoming events.
            var writer = new MongoDbProjectionWriter<DashboardView>(_applicationSettings.MongoDbConnectionString, _applicationSettings.MongoDbName);
            var id = Guid.NewGuid();
            var dashboardView = new DashboardView {Id = id, Name = req.Name};
            writer.Add(id, dashboardView);
            return Request.CreateResponse(HttpStatusCode.Created, "RAG Information Created in Dashboard DB");
        }
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