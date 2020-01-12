using System.Threading.Tasks;

namespace Terrier
{
    class Program
    {
        static Task Main(string[] args)
            => new Startup(args).RunAsync();
    }
}
