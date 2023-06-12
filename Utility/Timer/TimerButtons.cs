using System;
using UnityEngine;
using UnityEngine.UI;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Optional Timer Class: Manage all the UI Buttons
    /// </summary>
    public class TimerButtons : MonoBehaviour
    {
        [Header("Timer Script")]

        [SerializeField] private Timer _timerScript;

        [Header("Timer Buttons")]

        [SerializeField]
        private Button _startButton;
        [SerializeField]
        private Button _stopButton;
        [SerializeField]
        private Button _restartButton;


        private void OnEnable()
        {
            _timerScript.onTimerStarted += OnTimerStartedHandler;
            _timerScript.onTimerStopped += OnTimerStoppedHandler;
            _timerScript.onTimerRestarted += OnTimerRestartedHandler;
            _timerScript.onTimerButtonsCheck += OnTimerButtonsCheckHandler;
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeButtons();
        }

        private void OnDisable()
        {
            _timerScript.onTimerStarted -= OnTimerStartedHandler;
            _timerScript.onTimerStopped -= OnTimerStoppedHandler;
            _timerScript.onTimerRestarted -= OnTimerRestartedHandler;
            _timerScript.onTimerButtonsCheck -= OnTimerButtonsCheckHandler;
        }


        private void InitializeButtons()
        {
            // Timer Stopped initial setup
            _startButton.interactable = true;
            _stopButton.interactable = false;
            _restartButton.interactable = false;

            // Set On Click Listeners events
            _startButton.onClick.AddListener(_timerScript.StartTimer);
            _stopButton.onClick.AddListener(_timerScript.StopTimer);
            _restartButton.onClick.AddListener(_timerScript.RestartTimer);
        }

        /// <summary>
        /// onTimerStarted listener method
        /// </summary>
        private void OnTimerStartedHandler()
        {
            // Update UI Buttons
            _startButton.interactable = false;
            _stopButton.interactable = true;
            _restartButton.interactable = true;
        }

        /// <summary>
        /// onTimerStopped listener method
        /// </summary>
        private void OnTimerStoppedHandler()
        {
            // Update UI Buttons
            if (_timerScript.IsTimerReached())
                _startButton.interactable = false;
            else
                _startButton.interactable = true;
            _stopButton.interactable = false;
        }

        /// <summary>
        /// onTimerRestarted listener method
        /// </summary>
        private void OnTimerRestartedHandler()
        {
            // Update UI Buttons
            _startButton.interactable = true;
            _stopButton.interactable = false;
            _restartButton.interactable = false;
        }

        /// <summary>
        /// onTimerButtonsCheck listener method
        /// </summary>
        private void OnTimerButtonsCheckHandler()
        {
            if (_timerScript.IsTimerReached())
                _startButton.interactable = false;
            else
                _startButton.interactable = true;
        }
    }
}
