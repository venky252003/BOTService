using CustomerBot.State;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace CustomerBot
{
    public class SystemMessages
    {
        public async Task HandleAsync(ConnectorClient connector, Activity message)
        {
            switch (message.Type)
            {
                case ActivityTypes.ContactRelationUpdate:
                    HandleContactRelation(message);
                    break;
                case ActivityTypes.ConversationUpdate:
                    await HandleConversationUpdateAsync(connector, message);
                    break;
                case ActivityTypes.DeleteUserData:
                    await HandleDeleteUserDataAsync(message);
                    break;
                case ActivityTypes.Ping:
                    HandlePing(message);
                    break;
                case ActivityTypes.Typing:
                    HandleTyping(message);
                    break;
                default:
                    break;
            }
        }

        void HandleContactRelation(IContactRelationUpdateActivity activity)
        {
            if (activity.Action == "add")
            {
                // user added chatbot to contact list
            }
            else // activity.Action == "remove"
            {
                // user removed chatbot from contact list
            }
        }

        async Task HandleConversationUpdateAsync(
            ConnectorClient connector, IConversationUpdateActivity activity)
        {
            const string WelcomeMessage =
                "Welcome to BDA bot! Type your input as below requirement \n" +
                "\"New FA\" to add FA. \n " +
                "\"Change Amount\" to change firm award or BDA amount \n " +
                "\"Team\" to add fa in team \n" +
                "\"Submit Plan\" to submit your business plan \n " +
                "\"Q&A\" to FQA detail";

            Func<ChannelAccount, bool> isChatbot =
                channelAcct => channelAcct.Id == activity.Recipient.Id;

            if (activity.MembersAdded?.Any(isChatbot) ?? false)
            {
                Activity reply = (activity as Activity).CreateReply(WelcomeMessage);
                await connector.Conversations.ReplyToActivityAsync(reply);
            }

            if (activity.MembersRemoved?.Any(isChatbot) ?? false)
            {
                // to be determined
            }
        }

        async Task HandleDeleteUserDataAsync(Activity activity)
        {
            await new BDAState().DeleteAsync(activity);
        }

        // random methods to test different ping responses
        bool IsAuthorized(IActivity activity) => DateTime.Now.Ticks % 3 != 0;
        bool IsForbidden(IActivity activity) => DateTime.Now.Ticks % 7 == 0;

        void HandlePing(IActivity activity)
        {
            if (!IsAuthorized(activity))
                throw new HttpException(
                    httpCode: (int)HttpStatusCode.Unauthorized,
                    message: "Unauthorized");
            if (IsForbidden(activity))
                throw new HttpException(
                    httpCode: (int)HttpStatusCode.Forbidden,
                    message: "Forbidden");
        }

        void HandleTyping(ITypingActivity activity)
        {
            // user has started typing, but hasn't submitted message yet
        }
    }
}