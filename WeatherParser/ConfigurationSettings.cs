using System.Configuration;

namespace WeatherParser
{
    public static class ConfigurationSettings
    {
        private static string dbConnectionString;
        private static string dbProviderName;
        private static string urlParser;

        static ConfigurationSettings()
        {
            dbConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            dbProviderName = ConfigurationManager.ConnectionStrings["connectionstring"].ProviderName;
            urlParser = ConfigurationManager.ConnectionStrings["connectionstring"].ProviderName;
        }

        public static string DbConnectionString
        {
            get
            {
                return dbConnectionString;
            }
        }

        public static string DbProviderName
        {
            get
            {
                return dbProviderName;
            }
        }

        public static string UrlParser
        {
            get
            {
                return urlParser;
            }
        }

    }
}
