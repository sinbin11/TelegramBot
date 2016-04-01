using System;
using System.Linq;
using System.Text;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Engine
{
    public class Admin : CommandManager
    {
        public static CommandManager Manager { get; } = new Admin();

        public Admin()
        {
            Actions.Add(".clear", Clear);
            Actions.Add(".msg", Msg);
            Actions.Add(".addreply", AddReply);
            Actions.Add(".removereply", RemoveReply);
            Actions.Add(".help", AdminHelp);
            Actions.Add(".addadmin", AddAdmin);
            Actions.Add(".sethistorycount", SetHistoryCount);
            Actions.Add(".userdata", UserData);
            Actions.Add(".removeadmin", RemoveAdmin);
            Actions.Add(".removealladmins", RemoveAllAdmins);
        }

        private Message RemoveAllAdmins(Message message)
        {
            Config.AdminList.Clear();
            Config.AdminList.Add(Config.GlobalAdmin);
            return message.Reply("Adminler temizlendi.");
        }

        private Message UserData(Message message)
        {
            var command = message.Text.Replace(message.Text.Split(' ').First(), "").Trim();
            var users = Config.UserMessages.Where(s => s.From.Name.ToLower().Contains(command)).Select(s => s.From);

            var builder = new StringBuilder();
            foreach (var channelAccount in users)
            {
                builder.AppendLine(FormattedUserData(channelAccount).ToString());
            }

            return message.Reply(builder.AppendLine().AppendLine().ToString());
        }

        private Message RemoveReply(Message message)
        {
            var command = message.Text.Replace(message.Text.Split(' ').First(), "").Trim();
            if (Config.Commands.ContainsKey(command))
            {
                Config.Commands.Remove(command);

                return message.Reply($"`{command}` komutu kaldırıldı.");
            }

            return message.Reply("Kaldırılacak komut bulunamadı");
        }

        private Message SetHistoryCount(Message message)
        {
            int maxHistory;
            if (!int.TryParse(message.Text.Split(' ').LastOrDefault() ?? string.Empty, out maxHistory))
                maxHistory = 10;

            Config.MaxHistory = maxHistory;
            return message.Reply($"Geçmişe eklenecek kayıt sayısı {maxHistory} olarak ayarlandı.");
        }

        private Message AddAdmin(Message message)
        {
            var userIds = message.Text.Replace(message.Text.Split(' ').First(), " ").Split(new []{ ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (userIds.Length <= 0)
                message.Reply("Kullanıcı bilgisi gerekli.");

            foreach (var userId in userIds)
            {
                if (!Config.AdminList.Contains(userId))
                {
                    Config.AdminList.Add(userId);
                }
            }

            return message.Reply("Eklendi!");
        }

        private Message RemoveAdmin(Message message)
        {
            var userIds = message.Text.Replace(message.Text.Split(' ').First(), " ").Trim().Split(' ');

            if (userIds.Length <= 0)
                message.Reply("Kullanıcı bilgisi gerekli.");

            var users = userIds.Where(s => Config.AdminList.Contains(s) && !s.Equals(Config.GlobalAdmin)).ToList();

            if (users.Any())
            {
                Config.AdminList.RemoveAll(s => users.Contains(s));
                return message.Reply("Silindi!");
            }

            return message.Reply("Eşleşen hiç kayıt bulunamadı");
        }

        private Message AdminHelp(Message message)
        {
            return message.Reply(FormattedAdminHelpMessages());
        }

        private Message AddReply(Message message)
        {
            var texts = message.Text.Remove(0, message.Text.IndexOf(' ') + 1).Split('|');
            if (texts.Length == 2)
            {
                Config.Commands.Add(texts[0], texts[1]);
                return message.Reply("Eklendi!");
            }

            return message.Reply("Hatalı Format! `/.addreply Hello|Hello ?user?`");
        }

        private Message Msg(Message message)
        {
            if(Config.UserMessages.Any())
                return message.Reply(FormattedUserMessages());

            return message.Reply("Geçmiş boş");
        }

        private Message Clear(Message message)
        {
            Config.UserMessages.Clear();
            return message.Reply("Geçmiş silindi!");
        }

        private string FormattedAdminHelpMessages()
        {
            var reply = new StringBuilder();
            reply.Append("Kullanılabilir admin komutları:").AppendLine().AppendLine();

            foreach (var actions in Actions.Where(s => s.Key.StartsWith(".")))
            {
                reply.Append(actions.Key).AppendLine().AppendLine();
            }

            return reply.ToString();
        }

        private string FormattedUserMessages()
        {
            var reply = new StringBuilder();

            foreach (var message in Config.UserMessages)
            {
                reply.Append("ChannelConversationId: ") .Append("`").Append(message.ChannelConversationId)  .Append("`").AppendLine().AppendLine()
                     .Append("ChannelMessageId: ")      .Append("`").Append(message.ChannelMessageId)       .Append("`").AppendLine().AppendLine()
                     .Append("ConversationId: ")        .Append("`").Append(message.ConversationId)         .Append("`").AppendLine().AppendLine()
                     .Append("Id: ")                    .Append("`").Append(message.Id)                     .Append("`").AppendLine().AppendLine()
                     .Append("Created: ")               .Append("`").Append(message.Created)                .Append("`").AppendLine().AppendLine()
                     .Append(FormattedUserData(message.From))
                     .Append("Text: ")                  .Append("`").Append(message.Text)                   .Append("`").AppendLine().AppendLine()
                     .AppendLine("---")
                     .AppendLine();
            }

            return reply.ToString();
        }

        private StringBuilder FormattedUserData(ChannelAccount account)
        {
            return new StringBuilder()
                .Append("User Id: ")    .Append("`").Append(account.Id)     .Append("`").AppendLine().AppendLine()
                .Append("User Name: ")  .Append("`").Append(account.Name)   .Append("`").AppendLine().AppendLine();
        }
    }
}