using Microsoft.Owin;
using Owin;
using System.IO;
using System.Web;

[assembly: OwinStartupAttribute(typeof(PostoAssistenciaFull.Startup))]
namespace PostoAssistenciaFull
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            VerificarDiretorioFoto();
        }

        private void VerificarDiretorioFoto()
        {
            var diretorioApp = HttpRuntime.AppDomainAppPath;

            var diretorioFoto = $"{diretorioApp}\\fotos";

            if (!Directory.Exists(diretorioFoto))
                Directory.CreateDirectory(diretorioFoto);
        }
    }
}
