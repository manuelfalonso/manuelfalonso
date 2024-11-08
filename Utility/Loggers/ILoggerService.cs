using UnityEngine;

namespace SombraStudios.Shared.Utility.Loggers
{
    public interface ILoggerService
    {
        void Log(object message, Object sender = null);
    }
}
