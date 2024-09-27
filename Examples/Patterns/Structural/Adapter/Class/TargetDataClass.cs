namespace SombraStudios.Shared.Examples.Patterns.Structural.Adapter.Class
{
    // The Target defines the domain-specific class used by the client code.
    public class TargetDataClass
    {
        // Format 40.6976637,-74.119764
        string latitudeLongitude;

        public virtual string GetValue()
        {
            return latitudeLongitude;
        }
    }
}
