namespace Blueprinting.Tests.TestBlueprints
{
    public class AnObjectBlueprint : Blueprint<AnObject>
    {
        public override void ConfigureValidInstance()
        {
            Set(x => x.Name, "defaultName");
            Set(x => x.Position).StartingWith(0);
            Set(x => x.OddNumber).StartingWith(1, previous => (int)previous + 2);
            Set(x => x.SuccessiveString).StartingWith("000");
            Set(x => x.InnerObject).FromBlueprint();
        }
    }

    public class AnObject
    {
        public string PublicValue { get; set; }
        public string ProtectedValue { get; protected set; }
        public string PrivateValue { get; private set; }
        private string _backingField;
        public string BackingField { get { return _backingField; } }
        public string InvalidProperty { get { return PrivateValue; } }

        public string Name { get; set; }
        public int Position { get; set; }
        public int EvenNumber { get; set; }
        public int OddNumber { get; set; }
        public string SuccessiveString { get; set; }
        public object UnsupportedSequentialValue { get; set; }

        public AnInnerObject InnerObject { get; set; }
    }
}
