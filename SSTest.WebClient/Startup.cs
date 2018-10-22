using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSTest.WebClient.Startup))]
namespace SSTest.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
