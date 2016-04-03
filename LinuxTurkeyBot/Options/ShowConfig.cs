using System;
using CommandLine;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace LinuxTurkeyBot.Options
{
    [Verb("showconfig", HelpText = "This utility can be used to show current setting")]
    public class ShowConfig : ICommand
    {
        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            if (!message.From.IsAdmin()) return null;

            return message.CreateReplyMessage($"```{JsonConvert.SerializeObject(Config.Config.Current, Formatting.Indented)}```");
        }
    }
}