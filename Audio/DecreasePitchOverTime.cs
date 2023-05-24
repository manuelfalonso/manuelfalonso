using System.Collections;
using UnityEngine;

namespace SombraStudios.Audio
{

    /// <summary>
    /// This script sets the pitch of the audio at the start, 
    /// and then gradually turns it down to a desire value as time passes.
    /// </summary>
    public class DecreasePitchOverTime : MonoBehaviour
    {
        public void DecreasePitchCoroutine(AudioSource audioSource, float staringPitch, float finishingPitch, float time)
        {
            StartCoroutine(DecreasePitch(audioSource, staringPitch, finishingPitch, time));
        }


        private IEnumerator DecreasePitch(AudioSource audioSource, float staringPitch, float finishingPitch, float time)
        {
            audioSource.pitch = staringPitch;

            // While the pitch is over finishingPitch, decrease it as time passes.
            while (audioSource.pitch > finishingPitch)
            {
                audioSource.pitch -= Time.deltaTime * staringPitch / time;
                yield return new WaitForEndOfFrame();
            }
            audioSource.pitch = finishingPitch;
        }
    }
}