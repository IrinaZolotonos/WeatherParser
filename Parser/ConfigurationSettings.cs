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
            dbConnectionString = ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString;
            dbProviderName = ConfigurationManager.ConnectionStrings["MySQL"].ProviderName;
            urlParser = ConfigurationManager.AppSettings["url"];
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
