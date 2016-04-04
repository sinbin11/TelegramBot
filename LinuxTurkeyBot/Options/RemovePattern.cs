using System;
using CommandLine;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Options
{
    [Verb("removepattern", HelpText = "Remove pattern from conversation")]
    public class RemovePattern : ICommand
    {
        [Option('k', HelpText = "Keyword")]
        public string Keyword { get; set; }

        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            if (!message.From.IsAdmin()) return null;

            if (Config.Config.Current.Responses.ContainsKey(Keyword))
            {
                Config.Config.Current.Responses.Remove(Keyword);
                return message.CreateReplyMessage($"`{Keyword}` removed");
            }

            return message.CreateReplyMessage($"`{Keyword}` not found");
        }
    }
}