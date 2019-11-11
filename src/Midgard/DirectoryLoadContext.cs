using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Midgard
{
    public class DirectoryLoadContext : AssemblyLoadContext
    {
        private readonly string directory;

        public DirectoryLoadContext(string directory)
            : base("MidgardContext", true)
        {
            if (directory == null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            this.directory = directory;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            if (assemblyName is null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }

            if(assemblyName.Name == "Midgard.Minions.Abstractions")
            {
                return Default.LoadFromAssemblyName(assemblyName);
            }

            var assemblyPath = Path.Combine(this.directory, $"{assemblyName.Name}.dll");

            if (File.Exists(assemblyPath))
            {
                return this.LoadFromAssemblyPath(assemblyPath);
            }
            else
            {
                return null;
            }
        }
    }
}
