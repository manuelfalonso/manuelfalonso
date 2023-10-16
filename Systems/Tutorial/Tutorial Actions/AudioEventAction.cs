using System.Collections;
using UnityEngine;

namespace SombraStudios.TutorialSystem
{
    [CreateAssetMenu(fileName = "New Tutorial Action", menuName = "Sombra Studios/Tutorial/Audio Event Action", order = 4)]
    public class AudioEventAction : RaiseEventAction
    {
        [Header("Data")]
        [SerializeField] private bool _waitAudio = true;
        
        private AudioSource _audioSource;


        public override IEnumerator ExecuteAction()
        {
            if (_active == false) { yield break; }

            if (_gameEvent == null)
            {
                throw new MissingReferenceException();
            }

            _gameEvent.Raise(); // Set the Audio Source Here if necessary

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
