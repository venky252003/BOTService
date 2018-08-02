using BDAApp;
using BDAApp.Model;
using CustomerBot.Models;
using CustomerBot.State;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CustomerBot.Dialogs
{
    public class RegistrationDialog : IDialogResponse
    {
        BDAState mtgState = new BDAState();

        public async Task<string> GetResponseAsync(ConnectorClient connector, Activity activity)
        {
            UserData mtgData = await mtgState.GetAsync(activity);

            string response;

            switch (mtgData?.Method)
            {
                case nameof(HandleNameResponseAsync):
                    response = await HandleNameResponseAsync(activity, mtgData);
                    break;
                case nameof(HandlePersonResponseAsync):
                    response = await HandlePersonResponseAsync(activity, mtgData);
                    break;
                default:
                    response = StartRegistration(ref mtgData);
                    break;
            }

            await mtgState.UpdateAsync(activity, mtgData);

            return response;
        }

        string StartRegistration(ref UserData mtgData)
        {
            if (mtgData == null)
                mtgData = new UserData();

            mtgData.Dialog = nameof(RegistrationDialog);
            mtgData.Method = nameof(HandlePersonResponseAsync);

            return "What is your personId?";
        }

        async Task<string> HandlePersonResponseAsync(Activity activity, UserData mtgData)
        {
            int userPersonId = int.Parse(activity.Text);

            using (var ctx = new BDAContext())
            {
                Funding user =                    
                    (from usr in ctx.Fundings
                     where usr.personId == userPersonId
                     select usr).SingleOrDefault();
                    

                if (user == null)
                {
                    user = new Funding()
                    {
                        personId = userPersonId
                    };
                    ctx.Fundings.Add(user);
                }
                else
                {
                    user.personId = userPersonId;
                }

                await ctx.SaveChangesAsync();

                mtgData.UserChannelID = activity.From.Id;
                mtgData.UserDBID = user.personId;
            }

            mtgData.Method = nameof(HandleNameResponseAsync);

            return "What is your name?";
        }

        async Task<string> HandleNameResponseAsync(Activity activity, UserData mtgData)
        {
            using (var ctx = new BDAContext())
            {
                Funding user =                    
                    (from usr in ctx.Fundings
                     where usr.personId == mtgData.UserDBID
                     select usr)
                    .SingleOrDefault();

                if (user == null)
                {
                    user = new Funding()
                    {                        
                        FAName = activity.Text
                    };
                    ctx.Fundings.Add(user);
                }
                else
                {
                    user.FAName = activity.Text;
                }

                await ctx.SaveChangesAsync();

                mtgData.UserChannelID = activity.From.Id;
                mtgData.UserDBID = user.personId;
            }

            mtgData.Dialog = string.Empty;
            mtgData.Method = string.Empty;

            return "Registration succeeded!";
        }
    }
}