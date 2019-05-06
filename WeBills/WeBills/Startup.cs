using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeBills.Startup))]
namespace WeBills
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
