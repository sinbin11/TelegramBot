using System;
using System.Linq;
using CommandLine;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Options
{
    [Verb("ignore", HelpText = "Ignore user")]
    public class IgnorePattern : ICommand
    {
        [Option('i', HelpText = "Id", Required = true)]
        public string Id { get; set; }

        [Option('r', HelpText = "remove from ignore list")]
        public bool Remove { get; set; }

        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            if (!message.From.IsAdmin()) return null;

            if (string.IsNullOrEmpty(Id))
                return message.CreateReplyMessage("Id is required");

            if (Remove)
            {
                if (Config.Config.Current.IgnoreList.Any(s => s.Equals(Id)))
                {
                    Config.Config.Current.IgnoreList.Remove(Id);
                    return message.CreateReplyMessage($"`{Id}` removed from ignoring list");
                }

                return message.CreateReplyMessage($"`{Id}` doesn't found in ignoring list");
            }

            Config.Config.Current.IgnoreList.Add(Id);
            return message.CreateReplyMessage($"`{Id}` added ignoring list");
        }
    }
}