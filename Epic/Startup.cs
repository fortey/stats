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
                .UseSqlServerStorage(@"workstation id=statsbv.mssql.somee.com;packet size=4096;user id=fortey11_SQLLogin_1;pwd=vclvnzh46j;data source=statsbv.mssql.somee.com;persist security info=False;initial catalog=statsbv");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(() => StatsService.Start(), Cron.Hourly);
        }
    }
}
