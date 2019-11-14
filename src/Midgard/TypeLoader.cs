using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Midgard
{
    public class TypeLoader
    {
        private readonly DirectoryLoadContext loadContext;
        private Assembly assembly = null;

        public TypeLoader(string directory)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            this.loadContext = new DirectoryLoadContext(directory);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Type Load(TypeDefinition typeDefinition)
        {
            if (typeDefinition is null)
            {
                throw new ArgumentNullException(nameof(typeDefinition));
            }

            this.assembly = this.loadContext.LoadFromAssemblyName(typeDefinition.AssemblyName);
            return this.assembly.GetType(typeDefinition.Name);
        }

        public void Unload()
        {
            this.loadContext.Unload();
        }
    }
}
