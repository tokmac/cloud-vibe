using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cloud_Vibe.Startup))]
namespace Cloud_Vibe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
