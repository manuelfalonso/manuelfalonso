namespace SombraStudios.Shared.Examples.Patterns.Structural.Adapter.Class
{
    // The Adapter makes the Adaptee's data compatible with the Target's class.
    class Adapter : TargetDataClass
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee) => _adaptee = adaptee;

        public override string GetValue() => _adaptee.latitude + "," + _adaptee.longitude;
    }
}
