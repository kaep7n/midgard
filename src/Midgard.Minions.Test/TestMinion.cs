using Midgard.Minions.Abstractions;
using System;
using System.Threading.Tasks;

namespace Midgard.Minions.Test
{
    public class TestMinion : IMinion
    {
        public Task StartAsync()
        {
            Console.WriteLine("Start Test Minion");
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine("Stop Test Minion");
            return Task.CompletedTask;
        }
    }
}
