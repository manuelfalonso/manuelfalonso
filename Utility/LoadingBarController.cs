#if REQUIRES_EXTERNAL_PACKAGE
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Complete the loading bar and text depending of the amount of checkpoints invoked.
    /// Requieres:
    ///     DoTween (DG.Tweening)
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
            if (progressBar)
                DOTween.To(
                    () => progressBar.value,
                    x => progressBar.value = x,
                    ProgressBarValue,
                    animationDuration)
                    .OnComplete(() =>
                    {
                        if (ProgressBarValue == 1 && !loadingComplete)
                        {
                            //Debug.Log("LOADING BAR COMPLETED");
                            loadingComplete = true;
                            OnLoadingComplete?.Invoke();
                        }
                    }
                    );
        }

        private void AnimateText()
        {
            // Text Animation
            if (progressText)
                DOTween.To(
                    () => progress,
                    x => progress = x,
                    ProgressTextValue,
                    animationDuration);
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
#endif