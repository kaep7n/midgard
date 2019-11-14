using Microsoft.Extensions.DependencyInjection;
using Midgard.Hosting;
using System;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Midgard.Bootstrapper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((c, s) => {
                    s.AddHostedService<Core>();
                })
                .Build();

            await host.StartAsync()
                .ConfigureAwait(false);

            WriteContexts();

            await host.StopAsync()
                .ConfigureAwait(false);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            WriteContexts();
        }

        private static void WriteContexts()
        {
            foreach (var context in AssemblyLoadContext.All)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"context: {context.Name}");
                Console.ForegroundColor = ConsoleColor.Gray;
                
                foreach (var assembly in context.Assemblies)
                {
                    if (!assembly.FullName.StartsWith("Midgard", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    Console.WriteLine("     " + assembly.FullName);
                }
            }
        }
    }
}
