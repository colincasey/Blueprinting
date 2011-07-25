using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blueprinting.PropertySetters
{
    public class ValuePropertySetter : AbstractPropertySetter
    {
        private readonly object _value;

        public ValuePropertySetter(MemberExpression memberExpression, object value) : base(memberExpression)
        {
            _value = value;
        }

        public override void ApplyValue(object target, IDictionary<string, object> state)
        {
            SetValue(target, _value);
        }
    }
}
