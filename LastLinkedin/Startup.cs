using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LastLinkedin.Startup))]
namespace LastLinkedin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //app.MapSignalR();  //enable service of signalR

        }
    }
}
