using Blueprinting.Configuration;
using Blueprinting.Tests.TestBlueprints;
using NUnit.Framework;

namespace Blueprinting.Tests.Unit.Configuration
{
    [TestFixture]
    public class TheBlueprintTest
    {
        [Test]
        public void should_be_able_to_create_a_from_type_descriptor_using_generics()
        {
            var registration = TheBlueprint.From<AnObjectBlueprint>();
            Assert.IsInstanceOf<FromTypeDescriptor>(registration);
        }

        [Test]
        public void should_be_able_to_create_a_from_type_descriptor_using_the_typeof_operator()
        {
            var registration = TheBlueprint.From(typeof(AnObjectBlueprint));
            Assert.IsInstanceOf<FromTypeDescriptor>(registration);
        }
    }
}
