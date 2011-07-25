using System.Reflection;
using Blueprinting.Configuration;
using Blueprinting.Tests.ExternalBlueprints;
using Moq;
using NUnit.Framework;

namespace Blueprinting.Tests.Unit.Configuration
{
    [TestFixture]
    public class FromAssemblyDescriptorTest
    {
        [Test]
        public void should_be_able_to_register_blueprints_from_an_assembly_with_the_blueprint_provider()
        {
            var assembly = Assembly.GetAssembly(typeof (ExternalBlueprint));
            var fromAssemblyDescriptor = new FromAssemblyDescriptor(assembly);
            var mockProvider = new Mock<IBlueprintProvider>();

            fromAssemblyDescriptor.Register(mockProvider.Object);

            mockProvider.Verify(x => x.Add(typeof(ExternalBlueprint)), Times.Once());
        }
    }
}
