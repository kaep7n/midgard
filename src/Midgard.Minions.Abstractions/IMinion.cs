using System;
using System.Threading.Tasks;

namespace Midgard.Minions.Abstractions
{
    public interface IMinion
    {
        Task StartAsync();

        Task StopAsync();
    }
}
