using Discord;
using Finite.Commands;
using System.Threading.Tasks;

namespace Terrier.Commands
{
    public abstract class DiscordModuleBase : ModuleBase<DiscordCommandContext>
    {
        public Task ReplyAsync(string text = null, bool isTTS = false, Embed embed = null, RequestOptions options = null)
            => Context.Channel.SendMessageAsync(text, isTTS, embed, options);
        public Task ReplyAsync(Embed embed, RequestOptions options = null)
            => ReplyAsync(embed: embed, options);
    }
}
