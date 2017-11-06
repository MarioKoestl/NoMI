using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Nomi.Nomi_WebApp.Startup))]

namespace Nomi.Nomi_WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration.UseSqlServerStorage("NoMiDbContext");
            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
