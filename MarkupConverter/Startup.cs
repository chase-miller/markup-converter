using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarkupConverterWeb.Startup))]
namespace MarkupConverterWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
