using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Command
{
    public class CommandManager : MonoBehaviour
    {
        [Tooltip("Predetermined list of commands to execute in order")]
        [SerializeField] private List<ICommand> m_CommandSequence = new ();
        [Tooltip("Delay between command execution")]
        [SerializeField] private float m_DelayBetweenCommands = 0.0f;

        // The next index to execute from command sequence
        private int m_CurrentIndex = 0;
        private WaitForSeconds m_WaitForSeconds;

        private void Awake()
        {
            m_WaitForSeconds = new WaitForSeconds(m_DelayBetweenCommands);
        }

        public virtual void Execute()
        {
            if (m_CurrentIndex >= m_CommandSequence.Count)
                return;

            var activeCommand = m_CommandSequence[m_CurrentIndex];

            if (activeCommand == null)
                return;

            StartCoroutine(ExecuteRoutine(activeCommand));
        }

        public virtual void Undo()
        {
            if (m_CurrentIndex <= 0)
                return;

            var activeCommand = m_CommandSequence[m_CurrentIndex - 1];

            if (activeCommand == null)
                return;

            StartCoroutine(UndoRoutine(activeCommand));
        }

        protected virtual IEnumerator ExecuteRoutine(ICommand commandToExecute)
        {
            yield return m_WaitForSeconds;

            if (commandToExecute != null)
                commandToExecute.Execute();

            m_CurrentIndex++;
        }

        protected virtual IEnumerator UndoRoutine(ICommand commandToUndo)
        {
            yield return m_WaitForSeconds;

            if (commandToUndo != null)
                commandToUndo.Undo();

            m_CurrentIndex--;
        }
    }
}
