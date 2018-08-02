using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using CustomerBot.Models;
using CustomerBot.State;

namespace CustomerBot.Dialogs
{
    [Serializable]
    /*public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // Calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // Return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }
    }*/

    public class RootDialog : IDialogResponse
    {
        public async Task<string> GetResponseAsync(ConnectorClient connector, Activity activity)
        {
            //Activity typingActivity = activity.BuildTypingActivity();
            //await connector.Conversations.ReplyToActivityAsync(typingActivity);
            //await Task.Delay(millisecondsDelay: 10000);

            IDialogResponse dialog;

           UserData mtgData = await new BDAState().GetAsync(activity);

            switch (mtgData?.Dialog)
            {
                case nameof(RegistrationDialog):
                default:
                    dialog = new RegistrationDialog();
                    break;
            }

            return await dialog.GetResponseAsync(connector, activity);
        }
    }
}