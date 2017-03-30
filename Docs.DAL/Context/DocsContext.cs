using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;
using System.Configuration;
using Docs.Model.DB.Docs;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Docs.DAL.Context
{
    public class DocsDbConfig : DbConfiguration
    {
        public DocsDbConfig()
        {
            SqlConnectionFactory defaultFactory =
                new SqlConnectionFactory(ConfigurationManager.ConnectionStrings["Docs"].ConnectionString);

            this.SetDefaultConnectionFactory(defaultFactory);
        }
    }
    [DbConfigurationType(typeof(DocsDbConfig))]
    public class DocsContext: DbContextIndexed
    {
        public DbSet<Doc> Doc { get; set; }
        public DbSet<Case> Case { get; set; }
        public DbSet<Court> Court { get; set; }
        public DbSet<Instance> Instance { get; set; }
        public DbSet<DocType> TypeDoc { get; set; }
        public DocsContext()
        {
            Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Docs"].ConnectionString;
            Database.Initialize(false);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
