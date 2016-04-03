using System.Collections.Generic;
using System.IO;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace LinuxTurkeyBot.Config
{
    public class Config
    {
        public static string Path { get; } = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/data.json");
        public static Config Current { get; set; } = new Config();

        public string BotName { get; set; } = ConfigKey.BotName;
        public HashSet<string> AdminList { get; set; } = ConfigKey.BotAdminIdList;
        public List<Message> UserMessages { get; } = new List<Message>();
        public Dictionary<string, string> Responses { get; } = new Dictionary<string, string>();
        public int MaxHistory { get; set; } = 10;

        static Config()
        {
            Current.Restore();
        }

        public void Save()
        {
            if(!File.Exists(Path))
                File.Create(Path).Close();

            File.WriteAllText(Path, JsonConvert.SerializeObject(Current, Formatting.None));
        }

        public void Restore()
        {
            if(File.Exists(Path))
                Current = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path)) ?? Current;
        }
    }
}