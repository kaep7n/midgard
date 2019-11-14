using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Midgard
{
    public class Core : IHostedService
    {
        private List<TypeLoader> typeLoaders = new List<TypeLoader>();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var typeDefinition = new TypeDefinition("Midgard.Minions.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Midgard.Minions.Test.TestMinion");

            for (int i = 0; i < 5; i++)
            {
                var typeLoader = new TypeLoader(@"C:\Users\kaept\source\repos\kaep7n\midgard\src\Midgard.Minions.Test\bin\Debug\netcoreapp3.0");
                var type = typeLoader.Load(typeDefinition);

                this.typeLoaders.Add(typeLoader);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var typeLoader in this.typeLoaders)
            {
                typeLoader.Unload();
            }

            return Task.CompletedTask;
        }
    }
}
