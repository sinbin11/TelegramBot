using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Engine
{
    public abstract class CommandManager
    {
        public Dictionary<string, Func<Message, Message>> Actions { get; } = new Dictionary<string, Func<Message, Message>>();

        protected CommandManager()
        {
            Actions.Add("command", RunCommand);
            Actions.Add("help", Help);
        }

        private Message Help(Message message)
        {
            return message.Reply(FormattedHelpMessages());
        }

        private Message RunCommand(Message message)
        {
            return Config.Commands.ContainsKey(message.ClearText()) ? RunCommandReply(message) : message.Reply("Unknown Command");
        }

        private Message RunCommandReply(Message message)
        {
            if (Config.Commands.ContainsKey(message.ClearText()))
            {
                var response = Config.Commands[message.ClearText()];

                if (response.Contains("?user?"))
                    response = response.Replace("?user?", message.From.Name);

                return message.Reply(response);
            }

            return message.Reply("Bilinmeyen komut, `help` yazmayı dene");
        }

        private string FormattedHelpMessages()
        {
            var reply = new StringBuilder();
            reply.Append("Kullanılabilir komutlar:").AppendLine().AppendLine();

            foreach (var actions in Actions)
            {
                if(actions.Key == "command") continue;

                reply.Append(actions.Key).AppendLine().AppendLine();
            }

            foreach (var actions in Config.Commands)
            {
                reply.Append(actions.Key).AppendLine().AppendLine();
            }

            return reply.ToString();
        }

        public virtual Message Run(Message message)
        {
            var command = message.ClearText().Split(' ').First();
            return Actions.ContainsKey(command)
                ? Actions[command](message)
                : Actions["command"](message);
        }
    }
}