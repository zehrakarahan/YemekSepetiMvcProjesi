using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YemekSepetiUygulamasi.mvc.Startup))]
namespace YemekSepetiUygulamasi.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
