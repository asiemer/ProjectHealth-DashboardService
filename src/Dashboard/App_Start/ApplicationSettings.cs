using System;
using System.Configuration;

namespace Dashboard
{
    public interface IApplicationSettings
    {
        string MongoDbConnectionString { get; }
        string MongoDbName { get; }
    }

    public class ApplicationSettings : IApplicationSettings
    {
        public ApplicationSettings()
        {
            MongoDbConnectionString = FromAppSetting("MongoDbConnectionString");
            MongoDbName = FromAppSetting("MongoDbName");
        }

        public string MongoDbConnectionString { get; private set; }
        public string MongoDbName { get; private set; }

        protected static string FromConnectionStrings(string name)
        {
            var setting = ConfigurationManager.ConnectionStrings[name];
            return setting == null ? string.Empty : setting.ConnectionString;
        }

        protected static string FromAppSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName];
        }

        protected static int TryGetIntAppSetting(string key, int defaultValue = 0)
        {
            try { return Convert.ToInt32(ConfigurationManager.AppSettings[key]); }
            catch (Exception) { return defaultValue; }
        }
    }
}