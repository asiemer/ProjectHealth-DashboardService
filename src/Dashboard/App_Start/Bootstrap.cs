using System;
using System.IO;
using System.Web.Http;
using log4net;
using log4net.Config;
using Projects.Domain;
using Projects.Infrastructure;
using StatsdClient;
using StructureMap;
using WebApiContrib.IoC.StructureMap;

namespace Projects
{
    public static class Bootstrap
    {
        private static IRepository _repository;

        public static void Init()
        {
            InitLogging();
            ObjectFactory.Initialize(init =>
            {
                init.For<IRepository>().Use(c => _repository);
                init.For<IApplicationSettings>().Use<ApplicationSettings>();
                init.For<IUniqueKeyGenerator>().Use<UniqueKeyGenerator>();
                init.For<ILog>().Singleton().Use(c => LogManager.GetLogger("Projects"));
            });
            GlobalConfiguration.Configuration.DependencyResolver =
                new StructureMapResolver(ObjectFactory.Container);

            var metricsConfig = new MetricsConfig
            {
                StatsdServerName = "statsd.hostedgraphite.com",
                Prefix = "9f05a9e6-ebc5-49bd-90fa-0c8689e7fbbf.CM.Heartbeat.DEV.IterationZero"
            };

            Metrics.Configure(metricsConfig);
        }

        private static void InitLogging()
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
        }
    }
}