using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(library_jkpg.Startup))]
namespace library_jkpg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
