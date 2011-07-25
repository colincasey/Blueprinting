using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blueprinting.PropertySetters;
using Blueprinting.Tests.TestBlueprints;
using Moq;
using NUnit.Framework;

namespace Blueprinting.Tests.Unit
{
    [TestFixture]
    public class BuilderTest
    {
        [Test]
        public void should_be_able_to_build_an_instance_of_a_blueprinted_object()
        {
            var mockBlueprint = new Mock<IBlueprint<AnObject>>();
            mockBlueprint.Setup(x => x.Create()).Returns(new AnObject());

            var builder = new Builder<AnObject>(mockBlueprint.Object);

            var builtObject = builder.Build();
            Assert.IsInstanceOf<AnObject>(builtObject);
        }

        [Test]
        public void should_be_able_to_override_property_values()
        {
            var mockBlueprint = new Mock<IBlueprint<AnObject>>();
            var mockPropertySetterFactory = new Mock<IPropertySetterFactory<AnObject>>();
            var mockPropertySetter = new Mock<IPropertySetter>();

            mockBlueprint.Setup(x => x.Create()).Returns(new AnObject());
            mockPropertySetterFactory.
                Setup(x => x.CreateValueSetter(It.IsAny<Expression<Func<AnObject, string>>>(), "hello")).
                Returns(mockPropertySetter.Object);

            var builder = new Builder<AnObject>(mockBlueprint.Object, mockPropertySetterFactory.Object);
            builder.Set(x => x.Name, "hello").Build();

            mockPropertySetter.Verify(x => x.ApplyValue(
                It.IsAny<AnObject>(),
                It.IsAny<IDictionary<string, object>>()
            ), Times.Once());
        }

        [Test]
        public void should_be_able_to_copy_an_existing_object()
        {
            var source = new AnObject();
            var mockBlueprint = new Mock<IBlueprint<AnObject>>();
            var mockPropertySetterFactory = new Mock<IPropertySetterFactory<AnObject>>();
            var mockPropertySetter = new Mock<IPropertySetter>();

            mockPropertySetterFactory.Setup(x => x.CreateCopyObjectSetter(source)).Returns(mockPropertySetter.Object);

            var builder = new Builder<AnObject>(mockBlueprint.Object, mockPropertySetterFactory.Object);
            builder.Copy(source).Build();

            mockPropertySetter.Verify(x => x.ApplyValue(
                It.IsAny<AnObject>(),
                It.IsAny<IDictionary<string, object>>()
            ), Times.Once());
        }

        [Test]
        public void should_raise_error_if_attempting_to_copy_a_null_object()
        {
            var mockBlueprint = new Mock<IBlueprint<AnObject>>();
            var mockPropertySetterFactory = new Mock<IPropertySetterFactory<AnObject>>();

            var builder = new Builder<AnObject>(mockBlueprint.Object, mockPropertySetterFactory.Object);

            Assert.Throws<ArgumentNullException>(() => builder.Copy(null));
        }
    }
}
