using Finite.Commands;
using System.Linq;
using System.Threading.Tasks;
using Terrier.Services;

namespace Terrier.Commands
{
    public class TerrierModule : DiscordModuleBase
    {
        private readonly PluginManager _manager;

        public TerrierModule(PluginManager manager)
        {
            _manager = manager;
        }

        [Command("version", "ver")]
        public Task GetVersionAsync()
            => ReplyAsync($"Terrierbot v{TerrierConstants.Version}");

        [Command("plugins", "pl")]
        public Task GetPluginsAsync()
            => ReplyAsync($"**Plugins ({_manager.Plugins.Count()})**\n{string.Join(", ", _manager.Plugins.Select(x => x.Name))}");
    }
}
