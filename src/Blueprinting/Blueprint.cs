using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blueprinting.PropertySetters;
using Blueprinting.Util;

namespace Blueprinting
{
    public abstract class Blueprint<T> : IBlueprint<T> where T : class
    {
        private readonly IDictionary<string, object> _state = new Dictionary<string, object>();
        public IDictionary<string, object> State { get { return _state; } }

        private List<IPropertySetter> _defaultPropertySetters;
        protected List<IPropertySetter> DefaultPropertySetters
        {
            get
            {
                if (_defaultPropertySetters == null)
                {
                    _defaultPropertySetters = new List<IPropertySetter>();
                    ConfigureValidInstance();
                }
                return _defaultPropertySetters;
            }
        }

        private static IPropertySetterFactory<T> PropertySetterFactory
        {
            get { return new DefaultPropertySetterFactory<T>(); }
        }

        public T Create()
        {
            var instance = Activator.CreateInstance(typeof (T), true);
            foreach (var propertySetter in DefaultPropertySetters)
            {
                propertySetter.ApplyValue(instance, State);
            }
            return (T) instance;
        }

        public abstract void ConfigureValidInstance();

        protected void Set<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
        {
            var propertySetter = PropertySetterFactory.CreateValueSetter(expression, value);
            DefaultPropertySetters.Add(propertySetter);
        }

        protected PropertyDescriptor<T> Set<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var propertyDescriptor = PropertySetterFactory.CreatePropertyDescriptor(expression.GetMemberExpression());
            DefaultPropertySetters.Add(propertyDescriptor);
            return propertyDescriptor;
        }
    }
}
