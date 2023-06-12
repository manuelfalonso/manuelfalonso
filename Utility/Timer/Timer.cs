using System.Collections;
using UnityEngine;
using System;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Timer Main Class: Manges time settings and runs timer
    /// </summary>
    public class Timer : MonoBehaviour
    {
        // Events invoked with Timer actions: Start, Stop and Restart
        public event Action OnTimerStarted;
        public event Action OnTimerStopped;
        public event Action OnTimerRestarted;

        // Event invoked when the timer pass a second
        public event Action OnTimerTick;

        // Event invoked when the timer tiem was modified
        public event Action OnTimerModified;

        // Event invoked when the timer buttons must be updated
        public event Action OnTimerButtonsCheck;

        // Event invoked when the timer reaches 0
        public event Action OnTimerReached;

        public bool IsRunning { get; private set; }
        // Used for stopping and continue time without losing milliseconds
        private float _remainingTimeOfOneSecond = 1;

        // Timer time
        public int Seconds {
            get { return _seconds; }
            private set 
            {
                if (value > 60)
                    value = 60;
                else if (value < 0)
                    value = 0;

                _seconds = value;

                OnTimerModified?.Invoke();
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

                _minutes = value;
                OnTimerModified?.Invoke();
            }
        }
        public int Hours {
            get { return _hours; }
            private set
            {
                if (value < 0)
                    value = 0;

                _hours = value;
                OnTimerModified?.Invoke();
            }
        }

        // Timer setted time
        public int SecondsSetted { 
            get { return _secondsSetted; } 
            set
            {
                Seconds = value;
                _secondsSetted = value;
            } 
        }
        public int MinutesSetted {
            get { return _minutesSetted; }
            set
            {
                Minutes = value;
                _minutesSetted = value;
            }
        }
        public int HoursSetted
        {
            get { return _hoursSetted; }
            set
            {
                Hours = value;
                _hoursSetted = value;
            }
        }

        [Header("Timer Settings")]

        [SerializeField]
        private int _secondsSetted = 0;
        [SerializeField]
        private int _minutesSetted = 0;
        [SerializeField]
        private int _hoursSetted = 0;

        [Header("Debug")]

        [SerializeField]
        private bool _debugMode = false;

        private int _seconds = 0;
        private int _minutes = 0;
        private int _hours = 0;


        // Start is called before the first frame update
        void Start()
        {
            InitializeTime();
        }


        private void InitializeTime()
        {
            Seconds = SecondsSetted;
            Minutes = MinutesSetted;
            Hours = HoursSetted;
            CheckTimer();
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

                // Timer pass a second Event
                OnTimerTick?.Invoke();

                if (_debugMode)
                    Debug.Log(Seconds);

                // Check if final timer was reached
                if (IsTimerReached())
                {
                    OnTimerReached?.Invoke();

                    StopTimer();
                }
            }
        }


        /// <summary>
        /// Check time reaches 0
        /// </summary>
        /// <returns>If Hours, minutes and seconds reaches 0</returns>
        public bool IsTimerReached()
        {
            var secondsReached = Seconds == 0 ? true : false;
            var minutesReached = Minutes == 0 ? true : false;
            var hoursReached = Hours == 0 ? true : false;
            return secondsReached && minutesReached && hoursReached;
        }

        /// <summary>
        /// Invoke onTimerButtonsCheck action
        /// </summary>
        public void CheckTimer()
        {
            OnTimerButtonsCheck?.Invoke();
        }

        /// <summary>
        /// Start timer countdown and update UI
        /// </summary>
        public void StartTimer()
        {
            IsRunning = true;

            if (_debugMode)
                Debug.Log("Timer Started");
        
            // Start timer
            StartCoroutine(RunTimer());

            // Start Event
            OnTimerStarted?.Invoke();
        }

        /// <summary>
        /// Stop timer countdown and update UI
        /// </summary>
        public void StopTimer()
        {
            IsRunning = false;

            if (_debugMode)
                Debug.Log("Timer Stopped");

            // Stop timer
            StopAllCoroutines();

            // Stop Event
            OnTimerStopped?.Invoke();

            // Update UI Buttons
            CheckTimer();
        }

        /// <summary>
        /// Restart timer countdown to initial set time and update UI
        /// </summary>
        public void RestartTimer()
        {
            IsRunning = false;

            if (_debugMode)
                Debug.Log("Timer Restarted");

            // Stop timer
            StopAllCoroutines();

            // Set timer
            Seconds = SecondsSetted;
            Minutes = MinutesSetted;
            Hours = HoursSetted;

            // Restart Event
            OnTimerRestarted?.Invoke();
        }
    }
}
