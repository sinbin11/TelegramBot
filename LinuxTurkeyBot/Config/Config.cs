using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Config
{
    public class Config
    {
        public static Config Current { get; set; } = new Config();
        public static StringWriter Help { get; set; } = new StringWriter();

        public string BotName { get; set; } = ConfigKey.BotName;
        public List<string> AdminList { get; set; } = ConfigKey.BotAdminIdList;
        public List<Message> UserMessages { get; } = new List<Message>();
        public Dictionary<string, string> Responses { get; } = new Dictionary<string, string>();
        public int MaxHistory { get; set; } = 10;

        public Config()
        {
            Responses.Add("selam", "Sanada selam güzel insan");
            Responses.Add("ubuntu download link", "[Ubuntu full indir, bedava indir, kur, download](http://releases.ubuntu.com/15.10/ubuntu-15.10-desktop-amd64.iso)");
            //Restore();
        }

        public void Save()
        {
            throw new NotImplementedException("Try again later.");
        }

        public void Restore()
        {
            throw new NotImplementedException("Try again later.");
        }
    }
}