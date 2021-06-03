using UnityEngine;

public class PlayAtRandomPitchScript : MonoBehaviour
{
	// A single sound effect can sound like several different takes
	// by randomly varying the pitch by a small amout, for example
	// 5 percent in either direction.
	
	public void PlayAtRandomPitch(AudioSource audio, AudioClip clip, float minPitch = 0.95f, float maxPitch = 1.05f)
	{
		float originalPitch = audio.pitch;
		audio.pitch = Random.Range(minPitch, maxPitch);
		audio.PlayOneShot(clip);
		audio.pitch = originalPitch;
	}
}
