using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAApp.Model
{
    public class Funding
    {
        public int Id { get; set; }
        public string FAName { get; set; }
        public int personId { get; set; }
        public double FirmAward { get; set; }
        public double Initiation { get; set; }
        public double OtherFund { get; set; }
        public double VoluntaryBDAAmount { get; set; }
        public double VoluntaryBDAPct { get; set; }
        public double BeginAmount { get; set; }
        public double AvaliableAmount { get; set; }
        public double YTDExpense { get; set; }
    }
}
