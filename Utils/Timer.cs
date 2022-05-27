using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Timer Class with Input Time, Buttons and Methods
/// </summary>
public class Timer : MonoBehaviour
{
    // Useb by the input event fields
    private UnityAction<string> _setSecondsAction;
    private UnityAction<string> _setMinutesAction;
    private UnityAction<string> _setHoursAction;

    private int _seconds = 0;
    private int _minutes = 0;
    private int _hours = 0;
    private bool _isRunning;
    // Used for stopping and continue time without losing milliseconds
    private float _remainingTimeOfOneSecond = 1;

    public int Seconds {
        get { return _seconds; }
        private set 
        {
            if (value > 60)
                value = 60;
            else if (value < 0)
                value = 0;
                        
            if (_secondsInput)
                _secondsInput.text = value.ToString();

            _seconds = value;
        }
    }
    public int Minutes {
        get { return _minutes; }
        private set
        {
            if (value > 60)
                value = 60;
            else if (value < 0)
                value = 0;

            if (_secondsInput)
                _minutesInput.text = value.ToString();

            _minutes = value;
        }
    }
    public int Hours {
        get { return _hours; }
        private set
        {
            if (value < 0)
                value = 0;

            if (_secondsInput)
                _hoursInput.text = value.ToString();

            _hours = value;
        }
    }

    // Event invoked when the timer reaches 0
    public UnityEvent OnTimerReached;

    [Header("Timer Settings")]

    [SerializeField]
    private int _secondsSet = 0;
    [SerializeField]
    private int _minutesSet = 0;
    [SerializeField]
    private int _hoursSet = 0;

    [Header("Timer Inputs")]

    [SerializeField]
    private TMP_InputField _secondsInput;
    [SerializeField]
    private TMP_InputField _minutesInput;
    [SerializeField]
    private TMP_InputField _hoursInput;

    [Header("Timer Buttons")]

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _stopButton;
    [SerializeField]
    private Button _restartButton;

    [Header("Debug")]

    [SerializeField]
    private bool _debugMode = false;

    #region Unity Events

    // Start is called before the first frame update
    void Start()
    {
        InitializeTime();
        InitializeInputFields();
        InitializeButtons();
    }

    #endregion

    #region Private Methods

    private void InitializeTime()
    {
        Seconds = _secondsSet;
        Minutes = _minutesSet;
        Hours = _hoursSet;
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
        _setSecondsAction += SetSeconds;
        _secondsInput.onValueChanged.AddListener(_setSecondsAction);
        _setMinutesAction += SetMinutes;
        _minutesInput.onValueChanged.AddListener(_setMinutesAction);
        _setHoursAction += SetHours;
        _hoursInput.onValueChanged.AddListener(_setHoursAction);
    }

    private void InitializeButtons()
    {
        // Buttons interactability
        _stopButton.interactable = false;
        _restartButton.interactable = false;

        // Set On Click Listeners events
        _startButton.onClick.AddListener(StartTimer);
        _stopButton.onClick.AddListener(StopTimer);
        _restartButton.onClick.AddListener(RestartTimer);
    }

    private IEnumerator RunTimer()
    {
        if (_debugMode)
            Debug.Log(Seconds);

        while (!IsTimerReached())
        {
            // Save the remaining time for one second on each frame
            // Credits to Bunny83
            for (
                float remaingTime = _remainingTimeOfOneSecond; 
                remaingTime > 0; 
                remaingTime -= Time.deltaTime)
            {
                _remainingTimeOfOneSecond = remaingTime;
                yield return null;
            }
            // Reset remaining time
            _remainingTimeOfOneSecond = 1;

            // Update timer
            if (Seconds > 0)
            {
                Seconds--;
            }
            else if (Minutes > 0)
            {
                Minutes--;
                Seconds = 59;
            }
            else if (Hours > 0)
            {
                Hours--;
                Minutes = 59;
                Seconds = 59;
            }

            if (_debugMode)
                Debug.Log(Seconds);

            // Check if final timer was reached
            if (IsTimerReached())
            {
                if (OnTimerReached.GetPersistentEventCount() != 0)
                    OnTimerReached.Invoke();

                StopTimer();
            }
        }
    }

    private bool IsTimerReached()
    {
        var secondsReached = Seconds == 0 ? true : false;
        var minutesReached = Minutes == 0 ? true : false;
        var hoursReached = Hours == 0 ? true : false;
        return secondsReached && minutesReached && hoursReached;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Seconds setter. Cannot be called if the timer is running.
    /// </summary>
    /// <param name="value">Seconds</param>
    private void SetSeconds(string value)
    {
        if (_isRunning)
            return;

        Seconds = int.Parse(value);
        _secondsSet = int.Parse(value);

        if (IsTimerReached())
            _startButton.interactable = false;
        else
            _startButton.interactable = true;
    }

    /// <summary>
    /// Minutes setter. Cannot be called if the timer is running.
    /// </summary>
    /// <param name="value">Minutes</param>
    private void SetMinutes(string value)
    {
        if (_isRunning)
            return;

        Minutes = int.Parse(value);
        _minutesSet = int.Parse(value);

        if (IsTimerReached())
            _startButton.interactable = false;
        else
            _startButton.interactable = true;
    }

    /// <summary>
    /// Hour setter. Cannot be called if the timer is running.
    /// </summary>
    /// <param name="value">Hours</param>
    private void SetHours(string value)
    {
        if (_isRunning)
            return;

        Hours = int.Parse(value);
        _hoursSet = int.Parse(value);

        if (IsTimerReached())
            _startButton.interactable = false;
        else
            _startButton.interactable = true;
    }

    /// <summary>
    /// Start timer countdown and update UI
    /// </summary>
    public void StartTimer()
    {
        _isRunning = true;

        if (_debugMode)
            Debug.Log("Timer Started");
        
        // Start timer
        StartCoroutine(RunTimer());

        // Update UI Buttons
        _startButton.interactable = false;
        _stopButton.interactable = true;
        _restartButton.interactable = true;

        // Update UI Input fields
        _secondsInput.interactable = false;
        _minutesInput.interactable = false;
        _hoursInput.interactable = false;
    }

    /// <summary>
    /// Stop timer countdown and update UI
    /// </summary>
    public void StopTimer()
    {
        _isRunning = false;

        if (_debugMode)
            Debug.Log("Timer Stopped");

        // Stop timer
        StopAllCoroutines();

        // Update UI Buttons
        if (IsTimerReached())
            _startButton.interactable = false;
        else
            _startButton.interactable = true;
        _stopButton.interactable = false;

        // Update UI Input fields
        _secondsInput.interactable = true;
        _minutesInput.interactable = true;
        _hoursInput.interactable = true;
    }

    /// <summary>
    /// Restart timer countdown to initial set time and update UI
    /// </summary>
    public void RestartTimer()
    {
        _isRunning = false;

        if (_debugMode)
            Debug.Log("Timer Restarted");

        // Stop timer
        StopAllCoroutines();

        // Set timer
        Seconds = _secondsSet;
        Minutes = _minutesSet;
        Hours = _hoursSet;

        // Update UI Buttons
        _startButton.interactable = true;
        _stopButton.interactable = false;
        _restartButton.interactable = false;

        // Update UI Input fields
        _secondsInput.interactable = true;
        _minutesInput.interactable = true;
        _hoursInput.interactable = true;
    }

    #endregion
}
