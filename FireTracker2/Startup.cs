using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FireTracker2.Startup))]
namespace FireTracker2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
