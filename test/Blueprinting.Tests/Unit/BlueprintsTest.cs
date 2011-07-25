using System;
using Blueprinting.Tests.ExternalBlueprints;
using Blueprinting.Tests.TestBlueprints;
using Moq;
using NUnit.Framework;

namespace Blueprinting.Tests.Unit
{
    [TestFixture]
    public class BlueprintsTest
    {
        [TearDown]
        public void ClearBlueprints()
        {
            Blueprints.Clear();
        }

        [Test]
        public void should_automatically_use_the_default_provider_with_all_blueprints_automatically_loaded_by_default()
        {
            Assert.IsInstanceOf<DefaultBlueprintProvider>(Blueprints.BlueprintProvider);
            Assert.IsInstanceOf<AnObjectBlueprint>(Blueprints.BlueprintProvider.GetBlueprintFor<AnObject>());
            Assert.IsInstanceOf<ExternalBlueprint>(Blueprints.BlueprintProvider.GetBlueprintFor<ExternalObject>());
        }

        [Test]
        public void should_be_able_to_set_a_custom_blueprint_provider()
        {
            var mockProvider = new Mock<IBlueprintProvider>();
            Blueprints.BlueprintProvider = mockProvider.Object;
            Assert.AreEqual(mockProvider.Object, Blueprints.BlueprintProvider);
        }

        [Test]
        public void should_provide_a_convenience_method_for_creating_blueprints()
        {
            var mockProvider = new Mock<IBlueprintProvider>();
            var mockBuilder = new Mock<Builder<Object>>(null, null);
            Blueprints.BlueprintProvider = mockProvider.Object;

            mockProvider.Setup(x => x.CreateBuilder<Object>()).Returns(mockBuilder.Object);

            Assert.AreEqual(mockBuilder.Object, Blueprints.For<Object>());
        }
    }
}
