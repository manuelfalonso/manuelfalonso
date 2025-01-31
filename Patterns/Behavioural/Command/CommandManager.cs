using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Command
{
    public class CommandManager : MonoBehaviour
    {
        [Tooltip("Predetermined list of commands to execute in order")]
        [SerializeField] private List<ICommand> _commandSequence = new ();
        [Tooltip("Delay between command execution")]
        [SerializeField] private float _delayBetweenCommands = 0.0f;

        // The next index to execute from command sequence
        private int _currentIndex = 0;
        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_delayBetweenCommands);
        }

        public virtual void Execute()
        {
            if (_currentIndex >= _commandSequence.Count)
                return;

            var activeCommand = _commandSequence[_currentIndex];

            if (activeCommand == null)
                return;

            StartCoroutine(ExecuteRoutine(activeCommand));
        }

        public virtual void Undo()
        {
            if (_currentIndex <= 0)
                return;

            var activeCommand = _commandSequence[_currentIndex - 1];

            if (activeCommand == null)
                return;

            StartCoroutine(UndoRoutine(activeCommand));
        }

        protected virtual IEnumerator ExecuteRoutine(ICommand commandToExecute)
        {
            yield return _waitForSeconds;

            if (commandToExecute != null)
                commandToExecute.Execute();

            _currentIndex++;
        }

        protected virtual IEnumerator UndoRoutine(ICommand commandToUndo)
        {
            yield return _waitForSeconds;

            if (commandToUndo != null)
                commandToUndo.Undo();

            _currentIndex--;
        }
    }
}
