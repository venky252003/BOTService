using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CustomerBot.Dialogs
{
    public interface IDialogResponse
    {
        Task<string> GetResponseAsync(ConnectorClient connector, Activity activity);
    }
}