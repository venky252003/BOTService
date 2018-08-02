using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerBot.Models
{
    [Serializable]
    public class UserData
    {
        public string UserChannelID { get; set; }
        public int UserDBID { get; set; }
        public string Dialog { get; set; }
        public string Method { get; set; }
    }
}