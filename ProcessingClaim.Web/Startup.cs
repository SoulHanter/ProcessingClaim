using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProcessingClaim.Web.Startup))]
namespace ProcessingClaim.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
