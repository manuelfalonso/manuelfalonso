using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Command
{
    /// <summary>
    /// Abstract base class for command pattern implementation using ScriptableObject.
    /// </summary>
    public abstract class CommandScriptableObject : ScriptableObject
    {
        /// <summary>
        /// Tries to execute the command if it can be executed.
        /// </summary>
        public void TryExecute()
        {
            if (CanExecute())
            {
                Execute();
            }
        }

        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <returns>True if the command can be executed, otherwise false.</returns>
        protected abstract bool CanExecute();

        /// <summary>
        /// Executes the command.
        /// </summary>
        protected abstract void Execute();
    }
}
