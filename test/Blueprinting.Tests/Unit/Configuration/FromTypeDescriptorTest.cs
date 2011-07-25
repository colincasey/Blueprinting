using System;
using System.Collections.Generic;
using Blueprinting.Configuration;
using Moq;
using NUnit.Framework;

namespace Blueprinting.Tests.Unit.Configuration
{
    [TestFixture]
    public class FromTypeDescriptorTest
    {
        [Test]
        public void should_register_the_described_type_with_the_blueprint_provider()
        {
            var fromTypeDescriptor = new FromTypeDescriptor(typeof(TestBlueprint));
            var mockProvider = new Mock<IBlueprintProvider>();

            fromTypeDescriptor.Register(mockProvider.Object);

            mockProvider.Verify(x => x.Add(typeof(TestBlueprint)), Times.Once());
        }

        private class TestBlueprint : IBlueprint<Object>
        {
            public object Create() { throw new NotImplementedException(); }
            public object Create(string name) { throw new NotImplementedException(); }

            public IDictionary<string, object> State
            {
                get { throw new NotImplementedException(); }
            }

            public IBlueprintProvider GetProvider()
            {
                throw new NotImplementedException();
            }
        }
    }
}
