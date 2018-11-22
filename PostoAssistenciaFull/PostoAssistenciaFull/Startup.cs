using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PostoAssistenciaFull.Startup))]
namespace PostoAssistenciaFull
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
