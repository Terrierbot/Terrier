using System;
using Terrier;

namespace TestPlugin.Services
{
    public class TestService : ITerrierService
    {
        public string GetRandomString()
        {
            return new Guid().ToString();
        }

        public void Start()
        {
            Console.WriteLine("Starting");
        }

        public void Stop()
        {
            Console.WriteLine("Stopping");
        }
    }
}
