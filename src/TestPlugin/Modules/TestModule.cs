using Discord.Commands;
using System.Threading.Tasks;
using Terrier.Commands;
using TestPlugin.Services;

namespace TestPlugin.Modules
{
    public class TestModule : DiscordModuleBase
    {
        private readonly TestService _test;

        public TestModule(TestService test)
        {
            _test = test;
        }

        [Command("say")]
        public Task SayAsync([Remainder]string text)
            => ReplyAsync(text);

        [Command("test")]
        public Task TestServiceAsync()
            => ReplyAsync(_test.GetRandomString());
    }
}
