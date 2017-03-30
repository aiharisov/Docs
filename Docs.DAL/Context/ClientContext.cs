using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;
using System.Configuration;
using Docs.Model.DB.Client;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Docs.DAL.Context
{
    public class ClientDbConfig : DbConfiguration
    {
        public ClientDbConfig()
        {
            SqlConnectionFactory defaultFactory =
                new SqlConnectionFactory(ConfigurationManager.ConnectionStrings["Client"].ConnectionString);

            this.SetDefaultConnectionFactory(defaultFactory);
        }
    }
    [DbConfigurationType(typeof(ClientDbConfig))]
    public class ClientContext: DbContextIndexed
    {
        public DbSet<Client> Client { get; set; }
        public ClientContext()
        {
            Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Client"].ConnectionString;
            Database.Initialize(false);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
