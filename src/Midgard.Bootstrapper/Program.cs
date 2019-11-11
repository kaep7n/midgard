using Microsoft.Extensions.DependencyInjection;
using Midgard.Hosting;
using Midgard.Minions.Abstractions;
using System;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Midgard.Bootstrapper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var loadContext = new DirectoryLoadContext(@"C:\Users\kaept\source\repos\kaep7n\midgard\src\Midgard.Minions.Test\bin\Debug\netcoreapp3.0");
            var assemblyName = new AssemblyName("Midgard.Minions.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            var assembly = loadContext.LoadFromAssemblyName(assemblyName);
            var type = assembly.GetType("Midgard.Minions.Test.TestMinion");

            //var proxy = DispatchProxy.Create<IMinion, MinionProxy>();
            var minion = Activator.CreateInstance(type);

            //((MinionProxy)proxy).SetTarget(minion);

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((c, s) => {
                    s.AddHostedService<Core>();
                    s.AddSingleton((IMinion)minion);
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

                    Console.WriteLine("         assembly: " + assembly.FullName);
                }
            }
        }
    }
}
