using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetManager_V1.Startup))]
namespace BudgetManager_V1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
