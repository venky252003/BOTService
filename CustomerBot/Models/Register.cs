using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BDAApp;
using BDAApp.Model;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace CustomerBot.Models
{
    [Serializable]

    public class RegisterFA
    {
        [Prompt("Enter FA Name")]
        public string FAName;

        [Prompt("Enter Person Id")]
        //[Pattern("^[\\0-9]$")]
        public int PersonId;

        [Prompt("Enter Firm Award Amount")]
        //[Pattern("^[0-9]*\\.?[0-9]*$")]
        public double RecclubAmount;

        [Prompt("Enter Initiation Amount")]
        //[Pattern("^[0-9]*\\.?[0-9]*$")]
        public double InitAmount;

        [Prompt("Enter Other Funding Amount")]
        //[Pattern("^[0-9]*\\.?[0-9]*$")]
        public double OtherAmount;

        [Prompt("Enter Voluntary BDA Amount")]
        //[Pattern("^[0-9]*\\.?[0-9]*$")]
        public double VoluntaryAmount;

        [Prompt("Enter Voluntary BDA Percentage")]
        //[Pattern("^[0-9]*\\.?[0-9]*\\%$")]
        public double VoluntaryPct;

        public static IForm<RegisterFA> BuildForm()
        {
            OnCompletionAsyncDelegate<RegisterFA> processRegister = async (context, state) =>
            {
                IMessageActivity reply = context.MakeMessage();
                reply.Text = $"New FA {state.FAName} Created  at {DateTime.Now.ToShortTimeString()}, please be on time. ";
                Save(state);
                // Save State to database here...
                await context.PostAsync(reply);
            };

            return new FormBuilder<RegisterFA>()
                    .Message("Please enter your details.")                    
                    .OnCompletion(processRegister)
                    .Build();       

            
        }

        private static void Save(RegisterFA fa)
        {
            Funding fund = new Funding();
            fund.FAName = fa.FAName;
            fund.personId = fa.PersonId;
            fund.AvaliableAmount = fa.RecclubAmount + fa.InitAmount + fa.OtherAmount + fa.VoluntaryAmount;
            fund.BeginAmount = fund.AvaliableAmount;
            fund.FirmAward = fa.RecclubAmount;
            fund.Initiation = fa.InitAmount;
            fund.OtherFund = fa.OtherAmount;
            fund.VoluntaryBDAAmount = fa.VoluntaryAmount;
            fund.VoluntaryBDAPct = fa.VoluntaryPct;
            fund.YTDExpense = 0;

            using (var ctx = new BDAContext())
            {
                ctx.Fundings.Add(fund);
                ctx.SaveChanges();
            }
        }
    }
}