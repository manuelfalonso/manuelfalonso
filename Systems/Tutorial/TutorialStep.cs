using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.TutorialSystem
{
    /// <summary>
    /// Runs its logic with Coroutine
    /// </summary>
    [Serializable]
    public class TutorialStep
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _isActive;
        [SerializeField] private bool _showLogs;
        [SerializeField] private List<TutorialActionSO> _actions;
        
        private bool _completed;

        public bool Completed { get => _completed; private set => _completed = value; }


        public TutorialStep(string name, List<TutorialActionSO> actions)
        {
            _actions = actions;
            _name = name;
            _completed = false;
        }


        public IEnumerator ExecuteActions()
        {
            if (!_isActive) { yield break; }

            if (_showLogs)
                Utility.Loggers.Logger.Log($"{_name} Step Started");

            for (int i = 0; i < _actions.Count; i++)
            {
                if (_showLogs)
                    Utility.Loggers.Logger.Log($"{_actions[i].name} Started");

                yield return _actions[i].ExecuteAction();

                if (_showLogs)
                    Utility.Loggers.Logger.Log($"{_actions[i].name} Finished");
            }

            _completed = true;

            if (_showLogs)
                Utility.Loggers.Logger.Log($"{_name} Step Finished");

            yield return null;
        }
    }
}
