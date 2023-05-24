using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Audio
{

    /// <summary>
    /// Script Manager that uses FootstepAudioClipListSO to run foot step clips
    /// depending of the Tag of the floor by its collision.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class CharacterControllerFootstepManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _source;
        [SerializeField]
        private List<FootstepAudioClipListSO> stepsList;

        private List<AudioClip> _currentList;


        private void Start()
        {
            if (_source == null)
            {
                if (TryGetComponent(out AudioSource audioSource))
                {
                    _source = audioSource;
                }
            }
        }


        public void PlayStep()
        {
            if (_currentList != null)
            {
                AudioClip clip = _currentList[Random.Range(0, _currentList.Count)];
                _source.PlayOneShot(clip);
            }
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            for (int i = 0; i < stepsList.Count; i++)
            {
                if (hit.transform.tag == stepsList[i].Tag)
                {
                    _currentList = stepsList[i].Steps;
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

