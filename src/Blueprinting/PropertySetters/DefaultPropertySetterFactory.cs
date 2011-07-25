using System;
using System.Linq.Expressions;
using Blueprinting.Util;

namespace Blueprinting.PropertySetters
{
    public class DefaultPropertySetterFactory<T> : IPropertySetterFactory<T> where T : class
    {
        public PropertyDescriptor<T> CreatePropertyDescriptor(MemberExpression memberExpression)
        {
            return new PropertyDescriptor<T>(memberExpression, this);
        }

        public IPropertySetter CreateValueSetter<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
        {
            return new ValuePropertySetter(expression.GetMemberExpression(), value);
        }

        public IPropertySetter CreateSequentialPropertySetter(MemberExpression memberExpression, object initialValue, Func<object, object> nextValueGenerator)
        {
            return new SequentialPropertySetter(memberExpression, initialValue, nextValueGenerator);
        }

        public IPropertySetter CreateFromBlueprintSetter(MemberExpression memberExpression)
        {
            return new FromBlueprintSetter(memberExpression);
        }

        public IPropertySetter CreateCopyObjectSetter(object source)
        {
            return new CopyObjectSetter(source);
        }
    }
}
