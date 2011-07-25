namespace Blueprinting.Tests.TestBlueprints
{
    public class PrivateObjectBlueprint : Blueprint<PrivateObject>
    {
        public override void ConfigureValidInstance() { }
    }

    public class PrivateObject
    {
        private PrivateObject() { }
    }
}
