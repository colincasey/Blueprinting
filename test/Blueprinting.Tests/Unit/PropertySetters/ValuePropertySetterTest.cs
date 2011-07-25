using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blueprinting.PropertySetters;
using Blueprinting.Tests.TestBlueprints;
using Blueprinting.Util;
using NUnit.Framework;

namespace Blueprinting.Tests.Unit.PropertySetters
{
    [TestFixture]
    public class ValuePropertySetterTest
    {
        private static IDictionary<string, object> GetState()
        {
            return new Dictionary<string, object>();
        }

        [Test]
        public void should_be_able_to_set_public_properties()
        {
            var instance = new AnObject();
            Expression<Func<AnObject, string>> expression = x => x.PublicValue;
            var setter = new ValuePropertySetter(expression.GetMemberExpression(), "public");

            Assert.AreNotEqual("public", instance.PublicValue);
            setter.ApplyValue(instance, GetState());
            Assert.AreEqual("public", instance.PublicValue);
        }

        [Test]
        public void should_be_able_to_set_protected_properties()
        {
            var instance = new AnObject();
            Expression<Func<AnObject, string>> expression = x => x.ProtectedValue;
            var setter = new ValuePropertySetter(expression.GetMemberExpression(), "protected");

            Assert.AreNotEqual("protected", instance.ProtectedValue);
            setter.ApplyValue(instance, GetState());
            Assert.AreEqual("protected", instance.ProtectedValue);
        }

        [Test]
        public void should_be_able_to_set_private_properties()
        {
            var instance = new AnObject();
            Expression<Func<AnObject, string>> expression = x => x.PrivateValue;
            var setter = new ValuePropertySetter(expression.GetMemberExpression(), "private");

            Assert.AreNotEqual("private", instance.PrivateValue);
            setter.ApplyValue(instance, GetState());
            Assert.AreEqual("private", instance.PrivateValue);
        }

        [Test]
        public void should_be_able_to_set_properties_backed_by_a_field()
        {
            var instance = new AnObject();
            Expression<Func<AnObject, string>> expression = x => x.BackingField;
            var setter = new ValuePropertySetter(expression.GetMemberExpression(), "backingField");

            Assert.AreNotEqual("backingField", instance.BackingField);
            setter.ApplyValue(instance, GetState());
            Assert.AreEqual("backingField", instance.BackingField);
        }

        [Test]
        public void should_be_able_to_set_inner_properties()
        {
            var instance = new AnObject { InnerObject = new AnInnerObject() };
            Expression<Func<AnObject, string>> expression = x => x.InnerObject.InnerName;
            var setter = new ValuePropertySetter(expression.GetMemberExpression(), "anInnerObject");

            Assert.AreNotEqual("anInnerObject", instance.InnerObject.InnerName);
            setter.ApplyValue(instance, GetState());
            Assert.AreEqual("anInnerObject", instance.InnerObject.InnerName);
        }
        
        [Test]
        public void should_be_able_to_chain_setters_to_an_arbitrary_depth()
        {
            var instance = new AnObject
            {
                InnerObject = new AnInnerObject
                {
                    AnotherInnerObject = new AnotherInnerObject()
                }
            };
            Expression<Func<AnObject, string>> expression = x => x.InnerObject.AnotherInnerObject.InnerName;
            var setter = new ValuePropertySetter(expression.GetMemberExpression(), "anotherInnerObject");

            Assert.AreNotEqual("anotherInnerObject", instance.InnerObject.AnotherInnerObject.InnerName);
            setter.ApplyValue(instance, GetState());
            Assert.AreEqual("anotherInnerObject", instance.InnerObject.AnotherInnerObject.InnerName);
        }

        [Test]
        public void should_raise_error_if_property_does_not_have_a_setter_or_backing_field()
        {
            var instance = new AnObject();
            Expression<Func<AnObject, string>> expression = x => x.InvalidProperty;
            var setter = new ValuePropertySetter(expression.GetMemberExpression(), "invalid");
            Assert.Throws<InvalidOperationException>(() => setter.ApplyValue(instance, null));
        }
    }
}
