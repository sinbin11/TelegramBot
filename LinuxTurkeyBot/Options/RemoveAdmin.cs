using System;
using System.Linq;
using CommandLine;
using LinuxTurkeyBot.Config;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Options
{
    [Verb("removeadmin", HelpText = "Add new admin for bot")]
    public class RemoveAdmin : ICommand
    {
        [Option('i', HelpText = "User id")]
        public string Id { get; set; }

        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            if (!message.From.IsAdmin()) return null;

            if (Config.Config.Current.AdminList.Count <= 1)
                return message.CreateReplyMessage("I need least one admin.");

            if (ConfigKey.BotAdminIdList.Any(s => s.Equals(Id)))
                return message.CreateReplyMessage("I can't remove super admin.");

            if(!Config.Config.Current.AdminList.Contains(Id))
                return message.CreateReplyMessage("This id already doesn't have admin privileges.");

            Config.Config.Current.AdminList.Remove(Id);
            return message.CreateReplyMessage($"`{Id}` removed");
        }
    }
}