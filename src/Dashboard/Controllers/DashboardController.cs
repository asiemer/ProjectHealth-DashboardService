using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashboard.Domain.Dashboard;

namespace Dashboard.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        private readonly IDashboardApplicationService _dashboardApplicationService;

        public DashboardController(IDashboardApplicationService dashboardApplicationService)
        {
            _dashboardApplicationService = dashboardApplicationService;
        }

        [HttpGet]
        [Route("getRAGWidget")]
        public HttpResponseMessage GetRAGWidgetInformation()
        {
            return Request.CreateResponse(HttpStatusCode.Created, "Hi There");
        }
    }

    public class RAGColumnModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}