using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Terrier.Commands
{
    public abstract class DiscordModuleBase : ModuleBase<SocketCommandContext>
    {
        public Task ReplyAsync(Embed embed, RequestOptions options = null)
            => ReplyAsync("", false, embed, options);
    }
}
