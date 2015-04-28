using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskFlamingo.UI.Startup))]
namespace TaskFlamingo.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
