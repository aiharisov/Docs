using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;
using System.Configuration;
using Docs.Model.DB.Log;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Docs.DAL.Context
{
    public class LogDbConfig : DbConfiguration
    {
        public LogDbConfig()
        {
            SqlConnectionFactory defaultFactory =
                new SqlConnectionFactory(ConfigurationManager.ConnectionStrings["Log"].ConnectionString);

            this.SetDefaultConnectionFactory(defaultFactory);
        }
    }
    [DbConfigurationType(typeof(LogDbConfig))]
    public class LogContext: DbContextIndexed
    {
        public DbSet<Error> Error { get; set; }
        public DbSet<ScrapperLog> ScrapperLog { get; set; }
        public LogContext()
        {
            Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Log"].ConnectionString;
            Database.Initialize(false);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
    }
}
