using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using CustomerBot.Models;
using CustomerBot.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace CustomerBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>      
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            if (activity.Type == ActivityTypes.Message)
            {
                if (activity?.Type == ActivityTypes.Message)
                {
                    //string chatbotResponse = await new RootDialog().GetResponseAsync(connector, activity);

                    //Activity reply = activity.CreateReply(chatbotResponse);
                    //Activity reply = activity.BuildMessageActivity(chatbotResponse);

                    // await connector.Conversations.ReplyToActivityAsync(reply);

                    await Conversation.SendAsync(activity, () => RegisterDialog());
                }
            }
            else
            {
                await new SystemMessages().HandleAsync(connector, activity);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        internal static IDialog<RegisterFA> LUISDialog()
        {
            //return Chain.From(() => FormDialog.FromForm(RegisterFA.BuildForm()));
            return Chain.From(() => new LUISDialog(RegisterFA.BuildForm));
        }

        internal static IDialog<RegisterFA> RegisterDialog()
        {
            return Chain.From(() => FormDialog.FromForm<RegisterFA>(RegisterFA.BuildForm));
            //return Chain.From(() => new LUISDialog(BugReport.BuildForm));
        }
    }

}