using System;
using System.Collections.Generic;

namespace Blueprinting.Tests.ExternalBlueprints
{
    public class ExternalBlueprint : IBlueprint<ExternalObject>
    {
        public ExternalObject Create()
        {
            throw new NotImplementedException();
        }

        public ExternalObject Create(string name)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> State
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class ExternalObject { }
}
