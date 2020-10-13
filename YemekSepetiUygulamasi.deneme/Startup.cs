using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YemekSepetiUygulamasi.deneme.Startup))]
namespace YemekSepetiUygulamasi.deneme
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
