using System.Collections;
using UnityEngine;

/// <summary>
/// Attach this script to a GameObject.
/// Attach an AudioSource to your GameObject. 
/// Choose an audio clip in the AudioClip field.
/// This script sets the pitch of the audio at the start, 
/// and then gradually turns it down to a desire value as time passes.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class DecreasePitchOverTime : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private float _startingPitch = 1f;
    [SerializeField]
    private float _finishingPitch = 0f;
    [SerializeField]
    private float _timeToDecrease = 10f;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        _audioSource = GetComponent<AudioSource>();

        //Initialize the pitch
        _audioSource.pitch = _startingPitch;
    }

    public void DecreasePitchCoroutine()
    {
        StartCoroutine(DecreasePitch());
    }

    private IEnumerator DecreasePitch()
    {
        //While the pitch is over _finishingPitch, decrease it as time passes.
        while (_audioSource.pitch > _finishingPitch)
        {
            _audioSource.pitch -= Time.deltaTime * _startingPitch / _timeToDecrease;
            yield return new WaitForEndOfFrame();
        }
        _audioSource.pitch = _finishingPitch;
    }
}