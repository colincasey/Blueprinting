using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Blueprinting.PropertySetters
{
    public class PropertyDescriptor<T> : IPropertySetter where T : class
    {
        private readonly MemberExpression _memberExpression;
        private readonly IPropertySetterFactory<T> _propertySetterFactory;
        private IPropertySetter _propertySetter;

        public PropertyDescriptor(MemberExpression memberExpression, IPropertySetterFactory<T> propertySetterFactory)
        {
            _memberExpression = memberExpression;
            _propertySetterFactory = propertySetterFactory;
        }

        public void StartingWith(object initialValue)
        {
            StartingWith(initialValue, null);
        }

        public void StartingWith(object initialValue, Func<object, object> nextValueGenerator)
        {
            _propertySetter = _propertySetterFactory.CreateSequentialPropertySetter(_memberExpression, initialValue, nextValueGenerator);
        }

        public void FromBlueprint()
        {
            _propertySetter = _propertySetterFactory.CreateFromBlueprintSetter(_memberExpression);
        }

        public void ApplyValue(object target, IDictionary<string, object> state)
        {
            _propertySetter.ApplyValue(target, state);
        }
    }
}
