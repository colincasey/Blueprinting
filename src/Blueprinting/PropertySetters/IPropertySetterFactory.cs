using System;
using System.Linq.Expressions;

namespace Blueprinting.PropertySetters
{
    public interface IPropertySetterFactory<T> where T : class
    {
        PropertyDescriptor<T> CreatePropertyDescriptor(MemberExpression memberExpression);
        IPropertySetter CreateValueSetter<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value);
        IPropertySetter CreateSequentialPropertySetter(MemberExpression memberExpression, object initialValue, Func<object, object> nextValueGenerator);
        IPropertySetter CreateFromBlueprintSetter(MemberExpression memberExpression);
        IPropertySetter CreateCopyObjectSetter(object source);
    }
}
