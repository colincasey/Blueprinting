using System;
using System.Collections.Generic;

namespace Blueprinting.Configuration
{
    public class FromTypeDescriptor : FromDescriptor
    {
        private readonly Type _type; 

        public FromTypeDescriptor(Type type)
        {
            _type = type;
        }

        protected override IEnumerable<Type> GetBlueprintTypes()
        {
            return new [] { _type };
        }
    }
}
