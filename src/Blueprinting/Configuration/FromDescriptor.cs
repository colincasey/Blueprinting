using System;
using System.Collections.Generic;

namespace Blueprinting.Configuration
{
    public abstract class FromDescriptor : IBlueprintRegistration
    {
        protected abstract IEnumerable<Type> GetBlueprintTypes();

        public void Register(IBlueprintProvider blueprints)
        {
            foreach (var type in GetBlueprintTypes())
            {
                blueprints.Add(type);
            }
        }
    }
}
