using Blueprinting.PropertySetters;
using Blueprinting.Tests.TestBlueprints;
using NUnit.Framework;

namespace Blueprinting.Tests.Unit.PropertySetters
{
    [TestFixture]
    public class DefaultPropertySetterFactoryTest
    {
        [Test]
        public void should_create_a_value_property_setter()
        {
            var factory = new DefaultPropertySetterFactory<AnObject>();
            var propertySetter = factory.CreateValueSetter(x => x.Name, "test");
            Assert.IsInstanceOf<ValuePropertySetter>(propertySetter);
        }
    }
}
