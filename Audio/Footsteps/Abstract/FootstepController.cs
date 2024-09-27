using SombraStudios.Shared.Audio.Footsteps.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Audio.Footsteps.Abstract
{
    /// <summary>
    /// Uses FootstepAudioClipListSO to run foot step clips
    /// depending of the Tag of the floor by its collision.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public abstract class FootstepController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource _source;

        [Header("Data")]
        [SerializeField] protected List<FootstepAudioClipListSO> _stepsList = new List<FootstepAudioClipListSO>();

        protected List<AudioClip> _currentList = new List<AudioClip>();
        protected string _currentTag;


        protected virtual void Start()
        {
            SafeInitialization();
        }

        private void SafeInitialization()
        {
            if (_source == null)
            {
                if (TryGetComponent(out AudioSource audioSource))
                {
                    _source = audioSource;
                }
                else
                {
                    Debug.LogError($"AudioSource missing", this);
                    enabled = false;
                }
            }
        }


        // Can be asigned to Animations Events on a Walk or Run animation.
        /// <summary>
        /// Plays a random AudioClip from the current audio step list
        /// </summary>
        public void PlayStep()
        {
            if (_currentList != null)
            {
                AudioClip clip = _currentList[Random.Range(0, _currentList.Count)];
                _source.PlayOneShot(clip);
            }
        }


        /// <summary>
        /// Compares <param name="hitTransform"></param> tag to select the AudioClip list
        /// </summary>
        /// <param name="hitTransform">Transform of the floor hit</param>
        protected void SelectAudioClipList(Transform hitTransform)
        {
            // We have the audio list reference of the actual hit tag
            if (_currentList != null && hitTransform.CompareTag(_currentTag)) { return; }

            for (int i = 0; i < _stepsList.Count; i++)
            {
                if (hitTransform.CompareTag(_stepsList[i].Tag))
                {
                    _currentList = _stepsList[i].Steps;
                    _currentTag = _stepsList[i].Tag;
                    break;
                }
                else
                {
                    _currentList = null;
                }
            }
        }
    }
}
