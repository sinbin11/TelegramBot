using System.Threading.Tasks;
using System.Web.Http;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Controllers
{
    public class MessagesController : ApiController
    {
        public async Task<Message> Post([FromBody]Message message)
        {
            return message.Type == "Message" ? message.Run() : HandleSystemMessage(message);
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                var reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}