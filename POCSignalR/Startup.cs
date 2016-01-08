using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POCSignalR.Startup))]
namespace POCSignalR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureSignalR(app);
        }
    }
}
