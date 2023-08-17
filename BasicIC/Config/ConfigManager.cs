using BasicIC.Interfaces;
using System.Web.Configuration;

namespace BasicIC.Config
{
    public class ConfigManager : IConfigManager
    {
        public string Get(string nameConfig)
        {
            return WebConfigurationManager.AppSettings[nameConfig];
        }

        public static string StaticGet(string nameConfig)
        {
            return WebConfigurationManager.AppSettings[nameConfig];
        }
    }
}