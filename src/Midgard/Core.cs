using Microsoft.Extensions.Hosting;
using Midgard.Minions.Abstractions;
using System;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;

namespace Midgard
{
    public class Core : IHostedService
    {
        private readonly IMinion minion;

        public Core(IMinion minion)
        {
            this.minion = minion;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.minion.StartAsync().ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this.minion.StopAsync().ConfigureAwait(false);
        }
    }
}
