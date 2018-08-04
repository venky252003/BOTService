using CustomerBot.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CustomerBot.State
{
    public class BDAState
    {
        const string BDADataProperty = "BDAData";

        public async Task<UserData> GetAsync(Activity activity)
        {
            using (IStateClient stateClient = activity.GetStateClient())
            {
                IBotState chatbotState = stateClient.BotState;
                
                BotData chatbotData =
                    await chatbotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);

                return chatbotData.GetProperty<UserData>(BDADataProperty);
            }
        }

        public async Task UpdateAsync(Activity activity, UserData inData)
        {
            using (StateClient stateClient = activity.GetStateClient())
            {
                IBotState chatbotState = stateClient.BotState;
                BotData chatbotData = await chatbotState.GetUserDataAsync(
                    activity.ChannelId, activity.From.Id);

                UserData meetingData =
                    chatbotData.GetProperty<UserData>(BDADataProperty);

                if (meetingData == null)
                    meetingData = new UserData();

                meetingData.UserChannelID = activity.From.Id;
                meetingData.UserDBID = inData.UserDBID;
                meetingData.Dialog = inData.Dialog;
                meetingData.Method = inData.Method;

                chatbotData.SetProperty(BDADataProperty, data: meetingData);
                await chatbotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, chatbotData);
            }
        }

        public async Task DeleteAsync(Activity activity)
        {
            using (StateClient stateClient = activity.GetStateClient())
            {
                IBotState chatbotState = stateClient.BotState;

                await chatbotState.DeleteStateForUserAsync(activity.ChannelId, activity.From.Id);
            }
        }
    }
}