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
        [SerializeField] private List<TutorialAction> _actions;
        
        private bool _completed;

        public bool Completed { get => _completed; private set => _completed = value; }


        public TutorialStep(string name, List<TutorialAction> actions)
        {
            _actions = actions;
            _name = name;
            _completed = false;
        }


        public IEnumerator ExecuteActions()
        {
            if (!_isActive) { yield break; }

            Utility.Logger.Log(_showLogs, $"{_name} Step Started");

            for (int i = 0; i < _actions.Count; i++)
            {
                Utility.Logger.Log(_showLogs, $"{_actions[i].name} Started");

                yield return _actions[i].ExecuteAction();

                Utility.Logger.Log(_showLogs, $"{_actions[i].name} Finished");
            }

            _completed = true;

            Utility.Logger.Log(_showLogs, $"{_name} Step Finished");

            yield return null;
        }
    }
}
