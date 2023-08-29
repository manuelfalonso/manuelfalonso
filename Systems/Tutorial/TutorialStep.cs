using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using SombraStudios.Tools;

namespace SombraStudios.TutorialSystem
{
    /// <summary>
    /// Runs its logic with Coroutine
    /// </summary>
    [Serializable]
    public class TutorialStep
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _isActive;
        [SerializeField] private List<TutorialAction> _actions;
        //[SerializeField]
        private bool _completed;
        [SerializeField] private bool _showLogs;

        public bool Completed
        {
            get
            {
                return _completed;
            }
            set
            {
                _completed = value;
            }
        }


        public TutorialStep(string name, List<TutorialAction> actions)
        {
            _actions = actions;
            _name = name;
            _completed = false;
        }


        public IEnumerator ExecuteActions()
        {
            if (!_isActive) { yield break; }

            StaticLogger.Log(_showLogs, $"{_name} Step Started");

            for (int i = 0; i < _actions.Count; i++)
            {
                StaticLogger.Log(_showLogs, $"{_actions[i].name} Started");

                yield return _actions[i].ExecuteAction();

                // Use this instead of yield return to skip actions with SetActionComplete() Button
                // TODO the action will continue running in parallel. Stop it.
                //TutorialManager.Instance.StartCoroutine(_actions[i].ExecuteAction());
                //yield return new WaitUntil(() => _actions[i].IsCompleted == true);

                StaticLogger.Log(_showLogs, $"{_actions[i].name} Finished");
            }

            _completed = true;

            StaticLogger.Log(_showLogs, $"{_name} Step Finished");

            yield return null;
        }
    }
}
