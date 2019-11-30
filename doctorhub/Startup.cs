using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(doctorhub.Startup))]
namespace doctorhub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
