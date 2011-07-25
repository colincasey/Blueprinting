using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blueprinting.PropertySetters;

namespace Blueprinting
{
    public class Builder<T> where T : class
    {
        private readonly IBlueprint<T> _blueprint;
        private readonly List<IPropertySetter> _overrides = new List<IPropertySetter>();

        private readonly IPropertySetterFactory<T> _propertySetterFactory;
        private IPropertySetterFactory<T> PropertySetterFactory
        {
            get { return _propertySetterFactory; }
        }
        
        public Builder(IBlueprint<T> blueprint, IPropertySetterFactory<T> propertySetterFactory = null)
        {
            _blueprint = blueprint;
            _propertySetterFactory = propertySetterFactory ?? new DefaultPropertySetterFactory<T>();
        }

        public T Build()
        {
            var instance = _blueprint.Create();
            foreach (var propertyOverride in _overrides)
            {
                propertyOverride.ApplyValue(instance, _blueprint.State);
            }
            return instance;
        }

        public Builder<T> Set<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
        {
            var propertySetter = PropertySetterFactory.CreateValueSetter(expression, value);
            _overrides.Add(propertySetter);
            return this;
        }

        public Builder<T> Copy(T source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var propertySetter = PropertySetterFactory.CreateCopyObjectSetter(source);
            _overrides.Add(propertySetter);
            return this;
        }
    }
}
