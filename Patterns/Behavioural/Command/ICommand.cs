namespace SombraStudios.Shared.Patterns.Behavioural.Command
{
    /// <summary>
    /// This interface defines the minimum functionality for creating the command design pattern
    /// (for ScriptableObjects, MonoBehaviours, or general System.Objects).
    ///
    /// In the concrete class implementing the functionality, the Undo method contains the logic
    /// to restore the object's state after running the Execute method.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Undoes the command, restoring the object's state.
        /// </summary>
        void Undo();
    }
    
    /// <summary>
    /// This interface defines the minimum functionality for creating the command design pattern
    /// with a parameterized type.
    ///
    /// In the concrete class implementing the functionality, the Undo method contains the logic
    /// to restore the object's state after running the Execute method.
    /// </summary>
    /// <typeparam name="T">The type of data used by the command.</typeparam>
    public interface ICommand<T>
    {
        /// <summary>
        /// Executes the command with the provided data.
        /// </summary>
        /// <param name="data">The data needed for the command execution.</param>
        void Execute(T data);

        /// <summary>
        /// Undoes the command with the provided data, restoring the object's state.
        /// </summary>
        /// <param name="data">The data needed for the command undo operation.</param>
        void Undo(T data);
    }
}