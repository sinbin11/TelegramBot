using System;
using CommandLine;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Options
{
    [Verb("restoreconfig", HelpText = "This utility can be used to restore saved config")]
    public class RestoreConfig : ICommand
    {
        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            if (!message.From.IsAdmin()) return null;

            try
            {
                Config.Config.Current.Restore();
                return message.CreateReplyMessage("Restore Success");
            }
            catch (Exception)
            {
                return message.CreateReplyMessage("Restre Failed");
            }
        }
    }
}