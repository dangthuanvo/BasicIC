using BasicIC.Interfaces;
using System.Web.Configuration;

namespace Settings.Config
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