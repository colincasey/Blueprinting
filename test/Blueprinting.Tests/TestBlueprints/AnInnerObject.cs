namespace Blueprinting.Tests.TestBlueprints
{
    public class AnInnerObjectBlueprint : Blueprint<AnInnerObject>
    {
        public override void ConfigureValidInstance()
        {
            Set(x => x.InnerName, "inner");
        }
    }

    public class AnInnerObject
    {
        public string InnerName { get; set; }
        public AnotherInnerObject AnotherInnerObject { get; set; }
    }
}
