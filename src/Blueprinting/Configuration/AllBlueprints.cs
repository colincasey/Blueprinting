using System;
using System.Reflection;

namespace Blueprinting.Configuration
{
    public static class AllBlueprints
    {
        public static FromAssemblyDescriptor FromThisAssembly()
        {
            return FromAssembly(Assembly.GetCallingAssembly());
        }

        public static FromAssemblyDescriptor FromAssemblyContaining<T>()
        {
            return FromAssemblyContaining(typeof (T));
        }

        public static FromAssemblyDescriptor FromAssemblyContaining(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return FromAssembly(type.Assembly);
        }

        public static FromAssemblyDescriptor FromAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            return new FromAssemblyDescriptor(assembly);
        }
    }
}
