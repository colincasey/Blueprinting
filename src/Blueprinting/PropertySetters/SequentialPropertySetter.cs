using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Blueprinting.PropertySetters
{
    public class SequentialPropertySetter : AbstractPropertySetter
    {
        private const string CurrentValueKeyFormat = "{0}.{1}.CurrentValue";

        private readonly object _initialValue;
        private readonly Func<object, object> _nextValueGenerator;

        public SequentialPropertySetter(MemberExpression memberExpression, object initialValue, Func<object, object> nextValueGenerator) : base(memberExpression)
        {
            _initialValue = initialValue;
            _nextValueGenerator = nextValueGenerator ?? DefaultValueGenerator;
        }

        public override void ApplyValue(object target, IDictionary<string, object> state)
        {
            var currentValueKey = string.Format(CurrentValueKeyFormat, MemberExpression.Member.ReflectedType, MemberExpression.Member.Name);
            var previousValue = state.ContainsKey(currentValueKey) ? state[currentValueKey] : null;
            var currentValue = previousValue == null ? _initialValue : _nextValueGenerator(previousValue);
            state[currentValueKey] = currentValue;
            SetValue(target, currentValue);
        }

        private static object DefaultValueGenerator(object previous)
        {
            if (previous is int)
            {
                return NextInt((int) previous);
            }
            if (previous is string)
            {
                return NextString(previous as string);
            }

            throw new NotImplementedException(string.Format("no default generator implemented for object of type {0}, try using a custom generator function", previous.GetType()));
        }

        private static object NextInt(int previous)
        {
            return previous + 1;
        }

        private static object NextString(string previous)
        {
            Func<string, string> replaceNum = prev =>
            {
                var prevNum = int.Parse(prev);
                var nextNum = NextInt(prevNum);
                return new Regex(prevNum + "$").Replace(prev, nextNum.ToString());
            };

            var match = new Regex(@"^(\d+)$").Match(previous);
            if (match.Success)
            {
                var numPart = match.Groups[1];
                return replaceNum(numPart.Value);
            }

            match = new Regex(@"(.*)(\d+)$").Match(previous);
            if (match.Success)
            {
                var strPart = match.Groups[1];
                var numPart = match.Groups[2];
                return string.Format("{0}{1}", strPart.Value, replaceNum(numPart.Value));
            }

            return string.Format("{0}{1}", previous, 0);
        }
    }
}
