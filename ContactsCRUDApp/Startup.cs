using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactsCRUDApp.Startup))]
namespace ContactsCRUDApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
