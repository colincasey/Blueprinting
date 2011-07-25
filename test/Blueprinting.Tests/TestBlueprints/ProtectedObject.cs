namespace Blueprinting.Tests.TestBlueprints
{
    public class ProtectedObjectBlueprint : Blueprint<ProtectedObject>
    {
        public override void ConfigureValidInstance() { }
    }

    public class ProtectedObject
    {
        protected ProtectedObject() { }
    }
}
