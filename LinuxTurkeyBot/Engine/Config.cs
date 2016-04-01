using System.Collections.Generic;
using System.IO;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace LinuxTurkeyBot.Engine
{
    public class Config
    {
        public static Config Current { get; set; } = new Config();
        public List<string> BotNames { get; set; } = ConfigKey.BotNameList;
        public List<string> AdminList { get; set; } = ConfigKey.BotAdminIdList;
        public List<Message> UserMessages { get; } = new List<Message>();
        public Dictionary<string, string> Commands { get; } = new Dictionary<string, string>();
        public int MaxHistory { get; set; } = 10;

        public Config()
        {
            Restore();
        }

        public void Save()
        {
            File.WriteAllText(ConfigKey.ConfigPath, JsonConvert.SerializeObject(this));
        }

        public void Restore()
        {
            if (File.Exists(ConfigKey.ConfigPath))
                Current = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigKey.ConfigPath));
        }
    }
}