using System;
using CommandLine;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Options
{
    [Verb("saveconfig", HelpText = "This utility can be used to save current options manual")]
    public class SaveConfig : ICommand
    {
        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            try
            {
                //Config.Config.Current.Save();
                return message.CreateReplyMessage("Save Success");
            }
            catch(Exception ex)
            {
                return message.CreateReplyMessage($"Save Failed: {ex.Message}");
            }
        }
    }
}