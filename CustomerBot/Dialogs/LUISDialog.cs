using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CustomerBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;


namespace CustomerBot.Dialogs
{
    [LuisModel("a4417d0a-5b37-4d12-a3c5-97a2a6c70798", "38976a9dba09429a8d56e73229398ed4", domain: "westus.api.cognitive.microsoft.com", Staging = true)]
    public class LUISDialog : LuisDialog<RegisterFA>
    {
        private readonly BuildFormDelegate<RegisterFA> NewFARegister;

        public LUISDialog(BuildFormDelegate<RegisterFA> newFARegister)
        {
            this.NewFARegister = newFARegister;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't know what you mean.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
           context.Wait(MessageReceived);
        }

        [LuisIntent("NewFA")]
        public async Task Register(IDialogContext context, LuisResult result)
        {
            var enrollmentForm = new FormDialog<RegisterFA>(new RegisterFA(), this.NewFARegister, FormOptions.PromptInStart);
            context.Call<RegisterFA>(enrollmentForm, Callback);
        }


    }
}