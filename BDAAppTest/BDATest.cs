using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDAApp.Model;
using BDAApp;
using System.Collections.Generic;
using System.Linq;

namespace BDAAppTest
{
    [TestClass]
    public class BDATest
    {
        [TestMethod]
        public void AddFunding()
        {
            Funding fund = new Funding();
            fund.FAName = "Kyama";
            fund.personId = 2841141;
            fund.AvaliableAmount = 1000;
            fund.BeginAmount = 2000;
            fund.FirmAward = 1000;
            fund.Initiation = 500;
            fund.OtherFund = 100;
            fund.VoluntaryBDAAmount = 400;
            fund.VoluntaryBDAPct = 10.5;
            fund.YTDExpense = 1000;

            int expected = 2;
            int actual = 0;

            using (var ctx = new BDAContext())
            {
                ctx.Fundings.Add(fund);
                ctx.SaveChanges();

                actual = (from usr in ctx.Fundings
                          select usr).Count();

            }

            Assert.AreEqual(expected, actual);
            
        }
    }
}
