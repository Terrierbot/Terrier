using Finite.Commands;
using System.Threading.Tasks;

namespace Terrier.Commands
{
    public class TerrierModule : DiscordModuleBase
    {
        [Command("version", "ver")]
        public Task GetVersionAsync()
            => ReplyAsync($"Terrierbot v{TerrierConstants.Version}");
    }
}
