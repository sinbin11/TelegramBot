using System;
using CommandLine;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace LinuxTurkeyBot.Options
{
    [Verb("listpatterns", HelpText = "Add new pattern for conversation")]
    public class ListPatterns : ICommand
    {
        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            if (!message.From.IsAdmin()) return null;
            return message.CreateReplyMessage($"```{Environment.NewLine}{Environment.NewLine}{JsonConvert.SerializeObject(Config.Config.Current.Responses)}```");
        }
    }
}