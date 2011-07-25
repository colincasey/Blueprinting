using Blueprinting.Tests.TestBlueprints;
using NUnit.Framework;

namespace Blueprinting.Tests.Functional
{
    [TestFixture]
    public class Usages
    {
        [TearDown]
        public void ClearBlueprints()
        {
            Blueprints.Clear();
        }

        [Test]
        public void should_be_able_to_create_an_instance_of_an_object_with_a_public_constructor()
        {
            var publicObject = Blueprints.For<PublicObject>().Build();
            Assert.IsInstanceOf<PublicObject>(publicObject);
        }

        [Test]
        public void should_be_able_to_create_an_instance_of_an_object_with_a_protected_constructor()
        {
            var protectedObject = Blueprints.For<ProtectedObject>().Build();
            Assert.IsInstanceOf<ProtectedObject>(protectedObject);
        }

        [Test]
        public void should_be_able_to_create_an_instance_of_an_object_with_a_private_constructor()
        {
            var privateObject = Blueprints.For<PrivateObject>().Build();
            Assert.IsInstanceOf<PrivateObject>(privateObject);
        }

        [Test]
        public void should_create_instances_of_an_object_with_default_values_set()
        {
            var defaultObject = Blueprints.For<AnObject>().Build();
            Assert.AreEqual("defaultName", defaultObject.Name);
        }

        [Test]
        public void should_be_able_to_create_successive_values_for_a_property()
        {
            Assert.AreEqual(0, Blueprints.For<AnObject>().Build().Position);
            Assert.AreEqual(1, Blueprints.For<AnObject>().Build().Position);
        }

        [Test]
        public void should_be_able_to_generate_succesive_values_for_a_property_using_a_generator_function()
        {
            Assert.AreEqual(1, Blueprints.For<AnObject>().Build().OddNumber);
            Assert.AreEqual(3, Blueprints.For<AnObject>().Build().OddNumber);
        }

        [Test]
        public void should_be_able_to_create_any_referenced_inner_objects_from_a_blueprint()
        {
            var anObject = Blueprints.For<AnObject>().Build();
            Assert.AreEqual("inner", anObject.InnerObject.InnerName);
        }

        [Test]
        public void should_be_able_to_override_referenced_inner_objects_properties()
        {
            var anObject = Blueprints.For<AnObject>().Set(x => x.InnerObject.InnerName, "inner name override").Build();
            Assert.AreEqual("inner name override", anObject.InnerObject.InnerName);
        }

        [Test]
        public void should_be_able_to_copy_values_from_an_existing_object()
        {
            var anObject1 = Blueprints.For<CopyableObject>().Set(x => x.Name, "this is a copy").Build();
            var anObject2 = Blueprints.For<CopyableObject>().Copy(anObject1).Build();
            Assert.AreEqual(anObject1.Name, anObject2.Name);
            Assert.AreEqual(anObject1.Date, anObject2.Date);
        }
    }
}
