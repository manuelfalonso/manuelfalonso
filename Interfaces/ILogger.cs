namespace SombraStudios.Interfaces
{

    /// <summary>
    /// Encapsulation for a Debug Log
    /// Use a bool field to enable or disable all debug logs from a script
    /// </summary>
    public interface ILogger
    {
        public void Log(object message);
    }
}
