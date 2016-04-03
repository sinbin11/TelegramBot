using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;

namespace LinuxTurkeyBot.Config
{
    public static class ConfigKey
    {
        private const string botName = "BotName";
        private const string adminId = "AdminId";
        private const string configPath = "ConfigPath";

        public static string BotName => ConfigurationManager.AppSettings[botName];
        public static HashSet<string> BotAdminIdList => new HashSet<string>(GetList(adminId));
        public static string ConfigPath => ConfigurationManager.AppSettings[configPath];

        private static List<string> GetList(string key)
        {
            var resultList = new List<string>();
            var botNamesConfig = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(botNamesConfig))
                resultList.AddRange(JsonConvert.DeserializeObject<List<string>>(botNamesConfig));
            return resultList;
        }
    }
}