using System;

namespace Blueprinting.Tests.TestBlueprints
{
    public class CopyableObjectBlueprint : Blueprint<CopyableObject>
    {
        public override void ConfigureValidInstance()
        {
            Set(x => x.Name, "copyable");
            Set(x => x.Date, DateTime.Now);
        }
    }

    public class CopyableObject
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
