using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Engine
{
    public static class MessageHelpers
    {
        public static Dictionary<bool, Func<Message, Message>> Managers { get; } = new Dictionary<bool, Func<Message, Message>>();

        static MessageHelpers()
        {
            Managers.Add(true, Admin.Manager.Run);
            Managers.Add(false, User.Manager.Run);
        }

        public static Message Reply(this Message msg, string reply)
        {
            return msg.CreateReplyMessage(reply);
        }

        public static bool IsAdmin(this Message msg)
        {
            return  msg.ClearText().StartsWith(".") &&
                    Config.AdminList.Any(s => s.Equals(msg.From.Id));
        }

        public static string ClearText(this Message msg)
        {
            var message = msg.Mentions.Aggregate(msg.Text, (current, mention) => current.Replace(mention.Text, "")
                .Replace("@linuxturkeybot ", "").Trim());

            if (message.StartsWith("/"))
                message = message.Remove(0, 1);

            return message.ToLower();
        }

        public static Message Run(this Message msg)
        {
            return Managers[msg.IsAdmin()](msg);
        }
    }
}