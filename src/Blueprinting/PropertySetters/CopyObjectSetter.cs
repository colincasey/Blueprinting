using System.Collections.Generic;
using Blueprinting.Util;

namespace Blueprinting.PropertySetters
{
    public class CopyObjectSetter : IPropertySetter
    {
        private readonly object _source;

        public CopyObjectSetter(object source)
        {
            _source = source;
        }

        public void ApplyValue(object target, IDictionary<string, object> state)
        {
            var propertyInfos = _source.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var propertyValue = propertyInfo.GetValue(_source, null);
                target.SetValue(propertyInfo.Name, propertyValue);
            }
        }
    }
}
