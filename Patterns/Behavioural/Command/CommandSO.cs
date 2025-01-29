using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Command
{
    /// <summary>
    /// Abstract base class for command ScriptableObjects.
    /// Implements the ICommand interface.
    /// </summary>
    public abstract class CommandSO : ScriptableObject, ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Undoes the command, restoring the object's state.
        /// </summary>
        public abstract void Undo();
    }

    /// <summary>
    /// Abstract base class for parameterized command ScriptableObjects.
    /// Implements the ICommand interface with a parameterized type.
    /// </summary>
    /// <typeparam name="T">The type of data used by the command.</typeparam>
    public abstract class CommandSO<T> : ScriptableObject, ICommand<T>
    {
        /// <summary>
        /// Executes the command with the provided data.
        /// </summary>
        /// <param name="data">The data needed for the command execution.</param>
        public abstract void Execute(T data);

        /// <summary>
        /// Undoes the command with the provided data, restoring the object's state.
        /// </summary>
        /// <param name="data">The data needed for the command undo operation.</param>
        public abstract void Undo(T data);
    }
}