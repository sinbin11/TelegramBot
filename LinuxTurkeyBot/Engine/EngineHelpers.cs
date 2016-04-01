using System.Linq;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Engine
{
    public static class EngineHelpers
    {
        public static Message Reply(this Message msg, string reply)
        {
            return msg.CreateReplyMessage(reply);
        }

        public static bool IsAdmin(this Message msg)
        {
            return Config.Current.AdminList.Any(s => s.Equals(msg.From.Id));
        }

        public static string ClearText(this Message message)
        {
            var cleanMessage = message.Text.Replace("@linuxturkeybot ", "").Trim());

            if (cleanMessage.StartsWith("/"))
                cleanMessage = cleanMessage.Remove(0, 1);

            return cleanMessage.ToLower();
        }

        public static Message Run(this Message message)
        {
            if (message.IsAdmin())
                return Admin.Manager.Run(message);

            return User.Manager.Run(message);
        }
    }
}