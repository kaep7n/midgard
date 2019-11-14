using Midgard.Properties;
using System;
using System.Reflection;

namespace Midgard
{
    public class TypeDefinition
    {
        public TypeDefinition(string assemblyName, string typeName)
        {
            if (string.IsNullOrEmpty(assemblyName))
            {
                throw new ArgumentException(Resources.ArgumentMustNotBeNullOrEmpty, nameof(assemblyName));
            }

            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentException(Resources.ArgumentMustNotBeNullOrEmpty, nameof(typeName));
            }

            this.AssemblyName = new AssemblyName(assemblyName);
            this.Name = typeName;
        }

        public AssemblyName AssemblyName { get; }

        public string Name { get; }
    }
}
