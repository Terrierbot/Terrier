using Microsoft.Extensions.DependencyInjection;
using Terrier;
using TestPlugin.Services;

namespace TestPlugin
{
    public class TestPlugin : TerrierPlugin
    {
        public override string Name => "TestPlugin";
        public override string Description => "A plugin used for testing if everything is working.";
        public override string Version => ":)";

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<TestService>();
        }
    }
}
