using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(burgers2.Startup))]
namespace burgers2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
