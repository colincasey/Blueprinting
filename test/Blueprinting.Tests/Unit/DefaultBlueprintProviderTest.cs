using System;
using Blueprinting.Tests.TestBlueprints;
using NUnit.Framework;

namespace Blueprinting.Tests.Unit
{
    [TestFixture]
    public class DefaultBlueprintProviderTest
    {
        [Test]
        public void should_be_able_to_add_a_blueprint_using_the_typeof_operator()
        {
            var provider = new DefaultBlueprintProvider();
            provider.Add(typeof(AnObjectBlueprint));
            Assert.IsInstanceOf<AnObjectBlueprint>(provider.GetBlueprintFor<AnObject>());
        }

        [Test]
        public void should_raise_exception_if_trying_to_add_a_non_blueprint_type_with_using_the_typeof_operator()
        {
            var provider = new DefaultBlueprintProvider();
            Assert.Throws<ArgumentException>(() => provider.Add(typeof(Object)));
        }

        [Test]
        public void should_be_able_to_add_a_blueprint_using_generics()
        {
            var provider = new DefaultBlueprintProvider();
            provider.Add<AnObjectBlueprint>();
            Assert.IsInstanceOf<AnObjectBlueprint>(provider.GetBlueprintFor<AnObject>());
        }

        [Test]
        public void should_raise_exception_if_trying_to_add_a_non_blueprint_type_with_using_generics()
        {
            var provider = new DefaultBlueprintProvider();
            Assert.Throws<ArgumentException>(provider.Add<Object>);
        }

        [Test]
        public void should_raise_exception_if_trying_to_retrieve_a_blueprint_that_was_never_added()
        {
            var provider = new DefaultBlueprintProvider();
            Assert.Throws<Exception>(() => provider.GetBlueprintFor<AnObject>());
        }

        [Test]
        public void should_be_able_to_create_a_builder_for_a_blueprintable_object()
        {
            var provider = new DefaultBlueprintProvider();
            provider.Add<AnObjectBlueprint>();
            var builder = provider.CreateBuilder<AnObject>();
            Assert.IsInstanceOf<Builder<AnObject>>(builder);
        }
    }
}
