using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SplitBill.Startup))]
namespace SplitBill
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
