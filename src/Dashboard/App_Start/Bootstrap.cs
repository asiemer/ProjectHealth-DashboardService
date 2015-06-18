using System;
using System.IO;
using System.Web.Http;
using Dashboard.Domain;
using Dashboard.Infrastructure;
using Dashboard.ReadModel.Providers;
using Dashboard.ReadModel.Views;
using log4net;
using log4net.Config;
using NServiceBus;
using StatsdClient;
using StructureMap;
using WebApiContrib.IoC.StructureMap;

namespace Dashboard
{
    public static class Bootstrap
    {
        private static IRepository _repository;
        private static IApplicationSettings _applicationSettings = new ApplicationSettings();

        public static void Init()
        {
            InitLogging();
            InitNSB();
            ObjectFactory.Initialize(init =>
            {
                init.For<IRepository>().Use(c => _repository);
                init.For<IApplicationSettings>().Use(() => _applicationSettings);
                init.For<IUniqueKeyGenerator>().Use<UniqueKeyGenerator>();
                init.For<IRAGWidgetViewProvider>().Use<RAGWidgetViewProvider>();
                init.For<IProjectionWriter<RAGWidgetView>>().Use<MongoDbProjectionWriter<RAGWidgetView>>().Ctor<string>("connectionString").Is(_applicationSettings.MongoDbConnectionString).Ctor<string>("databaseName").Is(_applicationSettings.MongoDbName);
                init.For<ILog>().Singleton().Use(c => LogManager.GetLogger("Dashboard"));
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

        private static void InitNSB()
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Dashboard.Dashboard");
            busConfiguration.UseSerialization<XmlSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            Bus.Create(busConfiguration).Start();
        }

        private static void InitLogging()
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
        }
    }
}