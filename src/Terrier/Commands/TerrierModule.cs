using Discord;
using Discord.Commands;
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

        [Command("version"), Alias("ver")]
        public Task GetVersionAsync()
            => ReplyAsync($"Terrierbot v{TerrierConstants.Version}");

        [Command("plugins"), Alias("pl")]
        public Task GetPluginsAsync()
            => ReplyAsync($"**Plugins ({_manager.Plugins.Count()})**\n{string.Join(", ", _manager.Plugins.Select(x => x.Name))}");

        [Command("plugin")]
        public Task GetPluginAsync(string name)
        {
            var plugin = _manager.Plugins.SingleOrDefault(x => x.Name.ToLower() == name);
            if (plugin == null)
                return ReplyAsync($"Unable to find a plugin like `{name}`");
            return ReplyAsync(new EmbedBuilder()
                .WithTitle(plugin.Name)
                .WithDescription(plugin.Description)
                .WithFooter(plugin.Version)
                .Build());
        }
    }
}
