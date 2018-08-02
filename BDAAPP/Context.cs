using BDAApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAApp
{
    public class BDAContext : DbContext
    {
        public BDAContext() : base("BDADB")
        {
            //Database.SetInitializer<BDAContext>(new BDADBInitializer());
            //Database.SetInitializer<BDAContext>(new CreateDatabaseIfNotExists<BDAContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BDAContext>(new BDADBInitializer());
        }

        public DbSet<Funding> Fundings { get; set; }
    }

    public class BDADBInitializer : DropCreateDatabaseIfModelChanges<BDAContext>
    {
        protected override void Seed(BDAContext context)
        {
            context.Fundings.Add(new Funding()
            {
                FAName = "John Westmoreland",
                AvaliableAmount = 100,
                BeginAmount = 150,
                FirmAward = 100,
                Initiation = 0,
                OtherFund = 10,
                personId = 2841442,
                VoluntaryBDAAmount = 40,
                VoluntaryBDAPct = 0.25,
                YTDExpense = 50
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
