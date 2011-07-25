using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Blueprinting.Util
{
    public static class ReflectionHelpers
    {
        public static MemberExpression GetMemberExpression<T>(this Expression<T> expression)
        {
            var expressionBody = expression.Body as MemberExpression;
            if (expressionBody == null) throw new ArgumentException("expression needs to be a property accessor like x => x.Name");

            var member = expressionBody.Member;
            if (member.MemberType != MemberTypes.Property) throw new ArgumentException("expression can be used for property members");

            return expressionBody;
        }

        public static object GetMemberExpressionTarget(this object caller, MemberExpression memberExpression){

            var callStack = new Stack<PropertyInfo>();

            while (memberExpression.Expression is MemberExpression)
            {
                memberExpression = memberExpression.Expression as MemberExpression;
                callStack.Push((PropertyInfo)memberExpression.Member);
            }

            while (callStack.Count != 0)
            {
                var propertyInfo = callStack.Pop();
                caller = propertyInfo.GetValue(caller, null);
            }

            return caller;
        }

        public static void SetValue(this object target, string memberName, object value)
        {
            var type = target.GetType();
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var fieldName = string.Format("_{0}{1}", memberName.Substring(0, 1).ToLower(), memberName.Substring(1));
            var fieldInfo = type.GetField(fieldName, bindingFlags);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(target, value, bindingFlags, Type.DefaultBinder, null);
                return;
            }

            var propertyInfo = type.GetProperty(memberName, bindingFlags);
            if (propertyInfo != null && propertyInfo.GetSetMethod(true) != null)
            {
                propertyInfo.SetValue(target, value, bindingFlags, Type.DefaultBinder, null, null);
                return;
            }

            throw new InvalidOperationException("property name " + memberName + " cannot be set because no private field or setter could be found");
        }

        public static bool IsGenericTypeOf(this Type type, Type genericType)
        {
            return type.IsGenericType && genericType.IsGenericType && 
                   type.GetGenericTypeDefinition() == genericType.GetGenericTypeDefinition();
        }

        public static bool IsBlueprintType(this Type type)
        {
            return type.GetInterfaces().Any(x => x.IsGenericTypeOf(typeof(IBlueprint<>)));
        }

        public static Type GetBlueprintedType(this Type type)
        {
            var blueprintInterface = type.GetInterfaces().Where(x => x.IsGenericTypeOf(typeof(IBlueprint<>))).FirstOrDefault();
            return blueprintInterface != null ? blueprintInterface.GetGenericArguments()[0] : null;
        }
    }
}
