using System.Collections.Generic;

namespace Blueprinting.PropertySetters
{
    public interface IPropertySetter
    {
        void ApplyValue(object target, IDictionary<string, object> state);
    }
}
