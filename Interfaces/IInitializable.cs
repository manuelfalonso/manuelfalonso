using System;

namespace SombraStudios.Shared.Interfaces
{
    public interface IInitializable
    {
        bool IsInitialized { get; }
        void WhenInitialized(Action action);
    }
}
