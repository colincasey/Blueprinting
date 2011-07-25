using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blueprinting.Util;

namespace Blueprinting.Configuration
{
    public class FromAssemblyDescriptor : FromDescriptor
    {
        private readonly Assembly _assembly;

        public FromAssemblyDescriptor(Assembly assembly)
        {
            _assembly = assembly;
        }

        protected override IEnumerable<Type> GetBlueprintTypes()
        {
            return _assembly.IsDynamic ? Enumerable.Empty<Type>() : _assembly.GetExportedTypes().
                Where(x => x.IsBlueprintType() && !x.IsAbstract && !x.ContainsGenericParameters).
                ToList();
        }
    }
}
