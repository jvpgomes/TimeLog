using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JG.TimeLog.Web.Startup))]
namespace JG.TimeLog.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
