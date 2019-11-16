using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Midgard
{
    public class TypeLoader
    {
        private readonly WeakReference<DirectoryLoadContext> loadContextRef;
        private Assembly assembly = null;

        public TypeLoader(string directory)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            var loadContext = new DirectoryLoadContext(directory);
            this.loadContextRef = new WeakReference<DirectoryLoadContext>(loadContext);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Type Load(TypeDefinition typeDefinition)
        {
            if (typeDefinition is null)
            {
                throw new ArgumentNullException(nameof(typeDefinition));
            }

            if(this.loadContextRef.TryGetTarget(out var loadContext))
            { 
                this.assembly = loadContext.LoadFromAssemblyName(typeDefinition.AssemblyName);
                return this.assembly.GetType(typeDefinition.Name);
            }

            return null;
        }

        public void Unload()
        {
            if (this.loadContextRef.TryGetTarget(out var loadContext))
            {
                loadContext.Unload();
            }
        }
    }
}
