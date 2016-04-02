using System.Web.Http;
using LinuxTurkeyBot.Engine;
using Microsoft.Bot.Connector;

namespace LinuxTurkeyBot.Controllers
{
    public class MessagesController : ApiController
    {
        public Message Post([FromBody]Message message)
        {
            return message.Type == "Message" ? message.Run() : HandleSystemMessage(message);
        }

        private Message HandleSystemMessage(Message message)
        {
            switch (message.Type)
            {
                case "Ping":
                    var reply = message.CreateReplyMessage();
                    reply.Type = "Ping";
                    return reply;
                case "DeleteUserData":
                    break;
                case "BotAddedToConversation":
                    break;
                case "BotRemovedFromConversation":
                    break;
                case "UserAddedToConversation":
                    break;
                case "UserRemovedFromConversation":
                    break;
                case "EndOfConversation":
                    break;
            }

            return null;
        }
    }
}