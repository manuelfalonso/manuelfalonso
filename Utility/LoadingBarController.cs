using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Complete the loading bar and text depending of the amount of checkpoints invoked.
    /// </summary>
    public class LoadingBarController : MonoBehaviour
    {
        // Invoke me with the corresping Checkpoint name
        public static Action<string> OnLoadingCheckpointReached;
        public static Action OnLoadingComplete;

        [Header("Progress")]
        [SerializeField]
        private Slider progressBar;
        [SerializeField]
        private TextMeshProUGUI progressText;

        [Header("Checkpoints")]
        [SerializeField]
        private List<string> checkpointsList = new List<string>();

        [Header("Visuals")]
        [SerializeField]
        private float animationDuration = 2f;
        [SerializeField]
        [Tooltip("A fraction of the bar will autocomplete on start")]
        private bool preWarmProgress = true;

        private int progress = 0;
        private int CheckpointCount => 
            preWarmProgress ? checkpointsList.Count + 1 : checkpointsList.Count;
        private int checkpointsCompleted;
        private bool loadingComplete = false;
        private Coroutine barRoutine;
        private Coroutine textRoutine;

        // Internal use for checking up if the checkpoints were used
        private Dictionary<string, bool> checkpointsDictionary =
            new Dictionary<string, bool>();

        // Animation Values
        public float ProgressBarValue => 1f / CheckpointCount * checkpointsCompleted;
        public int ProgressTextValue => (int)(ProgressBarValue * 100);

        #region Unity Events
        private void OnEnable()
        {
            if (checkpointsList.Count == 0)
            {
                Debug.LogWarning($"Loading Bar - {name} checkpoints list cannot " +
                    $"be empty.");
            }

            // Pre warm values
            if (preWarmProgress)
                checkpointsCompleted = 1;
            else
                checkpointsCompleted = 0;

            AnimateBar();
            AnimateText();
        }

        private void Start()
        {
            // Complete Checkpoints dictionary with the info of Checkpoints List
            checkpointsList.ForEach((x) => checkpointsDictionary.Add(x, false));

            OnLoadingCheckpointReached +=
                LoadingBarController_OnLoadingCheckpointReached;
        }

        private void Update()
        {
            UpdateText();
        }

        private void OnDestroy()
        {
            OnLoadingCheckpointReached -=
                LoadingBarController_OnLoadingCheckpointReached;
        }
        #endregion

        #region Private Methods
        private void AnimateBar()
        {
            // Bar Animation
            if (!progressBar)
                return;

            // A new checkpoint supersedes whatever the previous one was still animating.
            if (barRoutine != null)
                StopCoroutine(barRoutine);

            barRoutine = StartCoroutine(AnimateBarRoutine(ProgressBarValue));
        }

        private IEnumerator AnimateBarRoutine(float target)
        {
            float start = progressBar.value;
            float elapsed = 0f;

            while (elapsed < animationDuration)
            {
                elapsed += Time.deltaTime;
                progressBar.value = Mathf.Lerp(start, target, elapsed / animationDuration);
                yield return null;
            }

            progressBar.value = target;
            barRoutine = null;

            if (Mathf.Approximately(target, 1f) && !loadingComplete)
            {
                loadingComplete = true;
                OnLoadingComplete?.Invoke();
            }
        }

        private void AnimateText()
        {
            // Text Animation
            if (!progressText)
                return;

            if (textRoutine != null)
                StopCoroutine(textRoutine);

            textRoutine = StartCoroutine(AnimateTextRoutine(ProgressTextValue));
        }

        private IEnumerator AnimateTextRoutine(int target)
        {
            float start = progress;
            float elapsed = 0f;

            while (elapsed < animationDuration)
            {
                elapsed += Time.deltaTime;
                progress = Mathf.RoundToInt(Mathf.Lerp(start, target, elapsed / animationDuration));
                yield return null;
            }

            progress = target;
            textRoutine = null;
        }

        private void UpdateText()
        {
            if (progressText)
                progressText.text = $"Loading {progress}%";
        }

        private void LoadingBarController_OnLoadingCheckpointReached(string checkpointName)
        {
            if (checkpointsList.Count == 0)
                return;

            // Checks if checkpoint exist, was already reached or can be completed
            CheckCheckpoint(checkpointName);
        }

        private void CheckCheckpoint(string checkpointName)
        {
            if (checkpointsDictionary.TryGetValue(checkpointName, out bool reached))
            {
                if (!reached)
                {
                    // Checkpoint reached
                    checkpointsCompleted++;
                    AnimateBar();
                    AnimateText();
                    checkpointsDictionary[checkpointName] = true;
                }
                else
                {
                    Debug.LogWarning($"Checkpoint name {checkpointName} was already " +
                        $"reached by the Loading Bar - {name}");
                }
            }
            else
            {
                Debug.LogWarning($"Checkpoint name {checkpointName} is not being " +
                    $"considered by the Loading Bar - {name}");
            }
        }
        #endregion
    }
}