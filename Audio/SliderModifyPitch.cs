using UnityEngine;

/// <summary>
/// A script that plays your chosen song. The pitch starts at 1.0.
/// You can increase and decrease the pitch and hear the change that is made.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PitchModifySlider : MonoBehaviour
{
    [SerializeField]
    private AudioClip mySong;

    private AudioSource audioSource;

    [SerializeField]
    private float pitchValue = 1.0f;
    [SerializeField]
    private float low = 0.75f;
    [SerializeField]
    private float high = 1.25f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = mySong;
        audioSource.loop = true;
        audioSource.Play();
    }

    void OnGUI()
    {
        pitchValue = GUI.HorizontalSlider(new Rect(25, 75, 100, 30), pitchValue, low, high);
        audioSource.pitch = pitchValue;
    }
}