using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Optional Timer Class: Manage all the UI input fields
    /// </summary>
    public class TimerInput : MonoBehaviour
    {
        [Header("Timer Script")]

        [SerializeField] private Timer _timerScript;

        [Header("Timer Inputs")]

        [SerializeField]
        private TMP_InputField _secondsInput;
        [SerializeField]
        private TMP_InputField _minutesInput;
        [SerializeField]
        private TMP_InputField _hoursInput;

        // Useb by the input event fields
        private UnityAction<string> _onSecondsUpdate;
        private UnityAction<string> _onMinutesUpdate;
        private UnityAction<string> _onHoursUpdate;


        private void OnEnable()
        {
            _timerScript.OnTimerStarted += OnTimerStartedHandler;
            _timerScript.OnTimerStopped += OnTimerStoppedHandler;
            _timerScript.OnTimerRestarted += OnTimerRestartedHandler;
            _timerScript.OnTimerTick += OnTimerTickOrModifiedHandler;
            _timerScript.OnTimerModified += OnTimerTickOrModifiedHandler;
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeInputFields();
        }

        private void OnDisable()
        {
            _timerScript.OnTimerStarted -= OnTimerStartedHandler;
            _timerScript.OnTimerStopped -= OnTimerStoppedHandler;
            _timerScript.OnTimerRestarted -= OnTimerRestartedHandler;
            _timerScript.OnTimerTick -= OnTimerTickOrModifiedHandler;
            _timerScript.OnTimerModified -= OnTimerTickOrModifiedHandler;
        }


        private void InitializeInputFields()
        {
            // Initialize format and validation
            _secondsInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
            _secondsInput.characterLimit = 2;
            _minutesInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
            _minutesInput.characterLimit = 2;
            _hoursInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
            _hoursInput.characterLimit = 2;

            // Initilialize On Value Changed Events
            _onSecondsUpdate += UpdateSeconds;
            _secondsInput.onValueChanged.AddListener(_onSecondsUpdate);
            _onMinutesUpdate += UpdateMinutes;
            _minutesInput.onValueChanged.AddListener(_onMinutesUpdate);
            _onHoursUpdate += UpdateHours;
            _hoursInput.onValueChanged.AddListener(_onHoursUpdate);
        }

        /// <summary>
        /// onTimerStarted listener method
        /// </summary>
        private void OnTimerStartedHandler()
        {
            // Update UI Input fields
            _secondsInput.interactable = false;
            _minutesInput.interactable = false;
            _hoursInput.interactable = false;
        }

        /// <summary>
        /// onTimerStopped listener method
        /// </summary>
        private void OnTimerStoppedHandler()
        {
            // Update UI Input fields
            _secondsInput.interactable = true;
            _minutesInput.interactable = true;
            _hoursInput.interactable = true;
        }

        /// <summary>
        /// onTimerRestarted listener method
        /// </summary>
        private void OnTimerRestartedHandler()
        {
            // Update UI Input fields
            _secondsInput.interactable = true;
            _minutesInput.interactable = true;
            _hoursInput.interactable = true;
        }

        /// <summary>
        /// onTimerTick listener method
        /// </summary>
        private void OnTimerTickOrModifiedHandler()
        {
            _secondsInput.text = _timerScript.Seconds.ToString();
            _minutesInput.text = _timerScript.Minutes.ToString();
            _hoursInput.text = _timerScript.Hours.ToString();
        }

        /// <summary>
        /// Seconds setter. Cannot be called if the timer is running.
        /// </summary>
        /// <param name="value">Seconds</param>
        private void UpdateSeconds(string value)
        {
            if (_timerScript.IsRunning)
                return;

            _timerScript.SecondsSetted = int.Parse(value);
            _timerScript.CheckTimer();
        }

        /// <summary>
        /// Minutes setter. Cannot be called if the timer is running.
        /// </summary>
        /// <param name="value">Minutes</param>
        private void UpdateMinutes(string value)
        {
            if (_timerScript.IsRunning)
                return;

            _timerScript.MinutesSetted = int.Parse(value);
            _timerScript.CheckTimer();
        }

        /// <summary>
        /// Hour setter. Cannot be called if the timer is running.
        /// </summary>
        /// <param name="value">Hours</param>
        private void UpdateHours(string value)
        {
            if (_timerScript.IsRunning)
                return;

            _timerScript.HoursSetted = int.Parse(value);
            _timerScript.CheckTimer();
        }
    }
}
