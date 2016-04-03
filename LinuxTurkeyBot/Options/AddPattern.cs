using System;
using CommandLine;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Options
{
    [Verb("addpattern", HelpText = "Add new pattern for conversation")]
    public class AddPattern : ICommand
    {
        [Option('k',  HelpText = "Keyword")]
        public string Keyword { get; set; }

        [Option('r', HelpText = "Response markdown text")]
        public string Response { get; set; }

        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            if (!message.From.IsAdmin()) return null;

            Config.Config.Current.Responses.Add(Keyword, Response);

            return message.CreateReplyMessage($"`{Keyword}` added");
        }
    }
}