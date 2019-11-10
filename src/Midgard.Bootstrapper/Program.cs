using Microsoft.Extensions.DependencyInjection;
using Midgard.Hosting;
using System.Threading.Tasks;

namespace Midgard.Bootstrapper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((c, s) => s.AddHostedService<Core>())
                .Build();

            await host.StartAsync()
                .ConfigureAwait(false);

            await host.StopAsync()
                .ConfigureAwait(false);
        }
    }
}
