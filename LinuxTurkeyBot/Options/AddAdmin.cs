using System;
using CommandLine;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Options
{
    [Verb("addadmin", HelpText = "Add new admin for bot")]
    public class AddAdmin : ICommand
    {
        [Option('i',  HelpText = "User id")]
        public string Id { get; set; }

        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            if (!message.From.IsAdmin()) return null;
            Config.Config.Current.AdminList.Add(Id);

            return message.CreateReplyMessage($"`{Id}` added");
        }
    }
}