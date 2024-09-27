namespace SombraStudios.Shared.Examples.Patterns.Structural.Adapter.Class
{
    // The Adaptee contains some useful behavior, but its data is
    // incompatible with the existing client code. The Adaptee needs some
    // adaptation before the client code can use it.
    class Adaptee
    {
        public string latitude;
        public string longitude;

        public Adaptee(string lat, string lon)
        {
            latitude = lat;
            longitude = lon;
        }
    }
}
