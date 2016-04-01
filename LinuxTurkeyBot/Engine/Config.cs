using System.Collections.Generic;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Engine
{
    public static class Config
    {
        public const string GlobalAdmin = "2BF1HdTSJBr";
        public static int MaxHistory { get; set; } = 10;
        public static List<Message> UserMessages { get; } = new List<Message>();
        public static List<string> AdminList { get; set; } = new List<string>(new []{ GlobalAdmin, "2c1c7fa3" });
        public static Dictionary<string, string> Commands { get; } = new Dictionary<string, string>();
    }
}