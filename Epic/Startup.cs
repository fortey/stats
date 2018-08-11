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
                .UseSqlServerStorage(@"");
            //app.UseHangfireDashboard();
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(() => StatsService.Start(), Cron.Hourly);
        }
    }
}
