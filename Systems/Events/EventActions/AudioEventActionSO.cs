using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Events
{
    [CreateAssetMenu(
        fileName = "AudioEventAction", 
        menuName = "Sombra Studios/Systems/Events/Audio Event Action", 
        order = 14)]
    public class AudioEventActionSO : RaiseEventActionSO
    {
        [Header("Data")]
        [SerializeField] private bool _waitAudio = true;
        
        private AudioSource _audioSource;

        public override IEnumerator StartAction()
        {
            if (!_active) 
                yield break;

            if (_gameEvent == null)
            {
                throw new MissingReferenceException();
            }

            _gameEvent.RaiseEvent(); // Set the Audio Source Here if necessary

            yield return null; 

            _audioSource.Play();

            if (_waitAudio)
            {
                yield return new WaitForSeconds(_audioSource.clip.length);
            }

            IsCompleted = true;
        }

        public void SetAudioSource(AudioSource audioSource) => _audioSource = audioSource;
    }
}
