using Finite.Commands;
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
        public Task SayAsync(params string[] words)
            => ReplyAsync(string.Join(" ", words));

        [Command("test")]
        public Task TestServiceAsync()
            => ReplyAsync(_test.GetRandomString());
    }
}
