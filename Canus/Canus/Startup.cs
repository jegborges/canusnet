using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Canus.Startup))]
namespace Canus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
