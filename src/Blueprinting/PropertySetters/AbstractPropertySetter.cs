using System.Collections.Generic;
using System.Linq.Expressions;
using Blueprinting.Util;


namespace Blueprinting.PropertySetters
{
    public abstract class AbstractPropertySetter : IPropertySetter
    {
        protected readonly MemberExpression MemberExpression;

        protected AbstractPropertySetter(MemberExpression memberExpression)
        {
            MemberExpression = memberExpression;
        }

        public virtual void SetValue(object target, object value)
        {
            target.GetMemberExpressionTarget(MemberExpression).SetValue(MemberExpression.Member.Name, value);
        }

        public abstract void ApplyValue(object target, IDictionary<string, object> state);
    }
}
