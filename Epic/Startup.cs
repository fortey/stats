using Epic.Domain.Models;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;


[assembly: OwinStartupAttribute(typeof(Epic.Startup))]
namespace Epic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DbContext>());
            ConfigureAuth(app);
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(@"workstation id=statistic.mssql.somee.com;packet size=4096;user id=fort3_SQLLogin_1;pwd=8dcok2hm58;data source=statistic.mssql.somee.com;persist security info=False;initial catalog=statistic");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(() => StatsService.Start(), Cron.Hourly);
        }
    }
}
