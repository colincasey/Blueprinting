using System;
using System.Reflection;
using Blueprinting.Configuration;
using NUnit.Framework;

namespace Blueprinting.Tests.Unit.Configuration
{
    [TestFixture]
    public class AllBlueprintsTest
    {
        [Test]
        public void should_be_able_to_load_blueprints_from_the_current_assembly()
        {
            var blueprintRegistration = AllBlueprints.FromThisAssembly();
            Assert.IsInstanceOf<FromAssemblyDescriptor>(blueprintRegistration);
        }

        [Test]
        public void should_be_able_to_load_blueprints_from_a_target_assembly()
        {
            var assembly = Assembly.GetAssembly(typeof (AllBlueprintsTest));
            var blueprintRegistration = AllBlueprints.FromAssembly(assembly);
            Assert.IsInstanceOf<FromAssemblyDescriptor>(blueprintRegistration);
        }

        [Test]
        public void should_raise_exception_when_loading_blueprints_from_a_target_assembly_that_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => AllBlueprints.FromAssembly(null));
        }

        [Test]
        public void should_be_able_to_load_blueprints_from_the_assembly_containing_the_target_type()
        {
            var blueprintRegistration = AllBlueprints.FromAssemblyContaining(typeof (AllBlueprintsTest));
            Assert.IsInstanceOf<FromAssemblyDescriptor>(blueprintRegistration);
        }

        [Test]
        public void should_be_able_to_load_blueprints_from_the_assembly_containing_the_target_type_using_generics()
        {
            var blueprintRegistration = AllBlueprints.FromAssemblyContaining<AllBlueprintsTest>();
            Assert.IsInstanceOf<FromAssemblyDescriptor>(blueprintRegistration);
        }

        [Test]
        public void should_raise_exception_when_loading_blueprints_from_a_target_type_that_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => AllBlueprints.FromAssemblyContaining(null));
        }
    }
}
