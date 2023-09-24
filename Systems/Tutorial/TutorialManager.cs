using SombraStudios.Patterns.Creational.Singleton;
using SombraStudios.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.TutorialSystem
{
    /// <summary>
    /// Manager for tutorial steps and its actions
    /// </summary>
    public class TutorialManager : Singleton<TutorialManager>
    {
        [Header("Data")]
        [SerializeField]
        private List<TutorialStep> _tutorialSteps = new List<TutorialStep>();

        [Header("Config")]
        [SerializeField]
        private bool _showTutorial = true;
        [SerializeField]
        private bool _saveProgression = true;
        [SerializeField]
        private float _startWaitInSeconds = 0f;

        [Header("Debug")]
        [SerializeField]
        private bool _showLogs = false;

        public int TutorialStepIndex() => _currentStepIndex;
        public bool IsTutorialCompleted() => _showTutorial && PlayerPrefs.GetInt(PlayerPrefsKeys.TUTORIAL_COMPLETED) == 1;
        public bool IsTutorialActive() => _showTutorial && PlayerPrefs.GetInt(PlayerPrefsKeys.TUTORIAL_COMPLETED) != 1;

        private int _currentStepIndex = 0;
                

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_startWaitInSeconds);

            if (_saveProgression)
                ResumeProgression();

            if (_showTutorial)
                yield return StartTutorial();

            yield break;
        }


        public void ResetTutoralSaveData()
        {
            ResetProgression();
        }


        private IEnumerator StartTutorial()
        {
            if (_tutorialSteps.Count == 0)
            {
                Tools.Logger.Log(_showLogs, $"No Tutorial found", this );
                yield break;
            }

            Tools.Logger.Log(_showLogs, $"Tutorial Started", this);

            for (_currentStepIndex = TutorialStepIndex(); _currentStepIndex < _tutorialSteps.Count; _currentStepIndex++)
            {
                yield return _tutorialSteps[_currentStepIndex].ExecuteActions();

                // Use this instead of yield return to skip steps with _completed field
                // TODO the step will continue running in parallel. Stop it.
                //StartCoroutine(_tutorialSteps[_currentStepIndex].ExecuteActions());
                //yield return new WaitUntil(() => _tutorialSteps[_currentStepIndex].Completed == true);

                SaveProgression();
            }

            Tools.Logger.Log(_showLogs, $"Tutorial Finished", this);

            SaveCompletion();

            yield break;
        }

        // Save methods
        private void SaveCompletion()
        {
            if (_saveProgression)
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.TUTORIAL_COMPLETED, 1);
            }
        }

        private void SaveProgression()
        {
            if (_saveProgression)
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.TUTORIAL_STEP, TutorialStepIndex());
            }
        }

        private void ResetProgression()
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKeys.TUTORIAL_COMPLETED);
            PlayerPrefs.DeleteKey(PlayerPrefsKeys.TUTORIAL_STEP);
            Tools.Logger.Log(_showLogs, $"TUTORIAL_COMPLETED and TUTORIAL_STEP keys deleted", this);
        }

        private void ResumeProgression()
        {
            _currentStepIndex = PlayerPrefs.GetInt(PlayerPrefsKeys.TUTORIAL_STEP);
        }
    }
}
