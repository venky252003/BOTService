using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerBot.State
{
    public class ConversationParameters
    {
        public ConversationAccount Conversation { get; set; }
        public ChannelAccount Chatbot { get; set; }
        public ChannelAccount User { get; set; }
        public string ServiceUrl { get; internal set; }
    }
}