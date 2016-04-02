using System;
using System.Text;
using CommandLine;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Options
{
    [Verb("whoami", HelpText = "This utility can be used to get user name and information")]
    public class WhoAmI : ICommand
    {
        [Option('i', "id", HelpText = "Displays information on the current user along with the identifier(Id).")]
        public bool NeedUserId { get; set; }

        public Message Respond(Message message, Parser parser, Type[] commands)
        {
            var response = new StringBuilder();
            if (this.NeedUserId)
            {
                response.Append("User Id: `").Append(message.From.Id).Append("`").AppendLine().AppendLine();
                response.Append("User Name: `").Append(message.From.Name).Append("`").AppendLine().AppendLine();
            }
            else
            {
                response.Append(message.From.Name);
            }

            return message.CreateReplyMessage(response.ToString());
        }
    }
}