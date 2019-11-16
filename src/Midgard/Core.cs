using Microsoft.Extensions.Hosting;
using Midgard.Minions.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Midgard
{
    public class Core : IHostedService
    {
        private readonly List<TypeLoader> typeLoaders = new List<TypeLoader>();
        private readonly List<IMinion> minions = new List<IMinion>();

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var typeDefinition = new TypeDefinition("Midgard.Minions.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Midgard.Minions.Test.TestMinion");

            for (int i = 0; i < 5; i++)
            {
                var typeLoader = new TypeLoader(@"C:\Users\kaept\source\repos\kaep7n\midgard\src\Midgard.Minions.Test\bin\Debug\netcoreapp3.0");
                var type = typeLoader.Load(typeDefinition);

                var minion = (IMinion)Activator.CreateInstance(type);
                this.minions.Add(minion);

                await minion.StartAsync()
                    .ConfigureAwait(false);

                this.typeLoaders.Add(typeLoader);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var minion in this.minions)
            {
                await minion.StopAsync()
                    .ConfigureAwait(false);
            }

            this.minions.Clear();

            foreach (var typeLoader in this.typeLoaders)
            {
                typeLoader.Unload();
            }
        }
    }
}
