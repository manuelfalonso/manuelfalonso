using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Command
{
    /// <summary>
    /// Abstract base class for command pattern implementation using ScriptableObject.
    /// </summary>
    public abstract class CommandSO : ScriptableObject
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

    /// <summary>
    /// Abstract base class for command pattern implementation using ScriptableObject.
    /// </summary>
    /// <typeparam name="T">The type of data that the command operates on.</typeparam>
    public abstract class CommandScriptableObject<T> : ScriptableObject where T : struct
    {
        /// <summary>
        /// Tries to execute the command if it can be executed.
        /// </summary>
        /// <param name="data">The data that the command operates on.</param>
        public void TryExecute(T? data = null)
        {
            if (CanExecute(data))
            {
                Execute(data);
            }
        }

        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <param name="data">The data that the command operates on.</param>
        /// <returns>True if the command can be executed, otherwise false.</returns>
        protected abstract bool CanExecute(T? data = null);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="data">The data that the command operates on.</param>
        protected abstract void Execute(T? data = null);
    }
}
