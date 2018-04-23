using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EagleFitApplicatie.App_Start.Startup))]
namespace EagleFitApplicatie.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}