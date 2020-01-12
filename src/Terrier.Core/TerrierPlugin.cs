using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Terrier
{
    public abstract class TerrierPlugin
    {
        public string PluginDirectory => Path.Combine(TerrierConstants.PluginsDirectory, Name);

        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Version { get; }

        public virtual void ConfigureServices(IServiceCollection services) { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
    }
}
