using System.Collections.Generic;
using System.IO;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace LinuxTurkeyBot.Config
{
    public class Config
    {
        public static FileInfo Path { get; } =
            new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/data.json"));

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
            if (Path.Directory?.Exists == false)
                Path.Directory?.Create();

            if (!Path.Exists)
                Path.Create().Close();

            File.WriteAllText(Path.FullName, JsonConvert.SerializeObject(Current, Formatting.None));
        }

        public void Restore()
        {
            if (Path.Exists)
                Current = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.FullName)) ?? Current;
        }
    }
}