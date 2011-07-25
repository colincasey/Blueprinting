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
    public class SequentialPropertySetterTest
    {
        [Test]
        public void should_be_able_to_create_an_instance_of_an_object_with_sequential_values()
        {
            var instance = new AnObject();
            var state = new Dictionary<string, object>();
            Expression<Func<AnObject, int>> expression = x => x.Position;
            var setter = new SequentialPropertySetter(expression.GetMemberExpression(), 0, null);

            setter.ApplyValue(instance, state);
            Assert.AreEqual(0, instance.Position);

            setter.ApplyValue(instance, state);
            Assert.AreEqual(1, instance.Position);
        }

        [Test]
        public void should_be_able_to_create_an_instance_of_an_object_with_sequential_values_generated_by_a_function()
        {
            var instance = new AnObject();
            var state = new Dictionary<string, object>();
            Expression<Func<AnObject, int>> expression = x => x.OddNumber;
            var setter = new SequentialPropertySetter(expression.GetMemberExpression(), 1, previous => (int) previous + 2);

            setter.ApplyValue(instance, state);
            Assert.AreEqual(1, instance.OddNumber);

            setter.ApplyValue(instance, state);
            Assert.AreEqual(3, instance.OddNumber);
        }
        
        [Test]
        public void should_be_able_to_generate_successive_string_values()
        {
            var instance = new AnObject();
            var state = new Dictionary<string, object>();
            Expression<Func<AnObject, string>> expression = x => x.SuccessiveString;
            var setter = new SequentialPropertySetter(expression.GetMemberExpression(), "010", null);

            setter.ApplyValue(instance, state);
            Assert.AreEqual("010", instance.SuccessiveString);

            setter.ApplyValue(instance, state);
            Assert.AreEqual("011", instance.SuccessiveString);
        }

        [Test]
        public void should_be_able_to_generate_successive_string_values_with_a_string_ending_with_a_number()
        {
            var instance = new AnObject();
            var state = new Dictionary<string, object>();
            Expression<Func<AnObject, string>> expression = x => x.SuccessiveString;
            var setter = new SequentialPropertySetter(expression.GetMemberExpression(), "person0", null);

            setter.ApplyValue(instance, state);
            Assert.AreEqual("person0", instance.SuccessiveString);

            setter.ApplyValue(instance, state);
            Assert.AreEqual("person1", instance.SuccessiveString);
        }

        [Test]
        public void should_be_able_to_generate_successive_string_values_with_non_numeric_starting_value()
        {
            var instance = new AnObject();
            var state = new Dictionary<string, object>();
            Expression<Func<AnObject, string>> expression = x => x.SuccessiveString;
            var setter = new SequentialPropertySetter(expression.GetMemberExpression(), "person", null);

            setter.ApplyValue(instance, state);
            Assert.AreEqual("person", instance.SuccessiveString);

            setter.ApplyValue(instance, state);
            Assert.AreEqual("person0", instance.SuccessiveString);
        }

        [Test]
        public void should_raise_an_error_if_the_generator_does_not_know_how_to_generate_sequential_values()
        {
            var instance = new AnObject();
            var state = new Dictionary<string, object>();
            var initialValue = new object();
            Expression<Func<AnObject, object>> expression = x => x.UnsupportedSequentialValue;
            var setter = new SequentialPropertySetter(expression.GetMemberExpression(), initialValue, null);

            setter.ApplyValue(instance, state);
            Assert.AreEqual(initialValue, instance.UnsupportedSequentialValue);

            Assert.Throws<NotImplementedException>(() => setter.ApplyValue(instance, state));
        }

        //[Test]
        //public void should_be_able_to_generate_retain_successive_values_across_blueprint_instances()
        //{
        //    var blueprints = Blueprints.Load(AllBlueprints.FromThisAssembly());

        //    var anObject1 = blueprints.For<AnObject>().Build();
        //    Assert.AreEqual(0, anObject1.InnerObject.InnerSuccessiveValue);

        //    var anObject2 = blueprints.For<AnObject>().Build();
        //    Assert.AreEqual(1, anObject2.InnerObject.InnerSuccessiveValue);
        //}
        
    }
}
