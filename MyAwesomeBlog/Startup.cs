using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyAwesomeBlog.Startup))]
namespace MyAwesomeBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
