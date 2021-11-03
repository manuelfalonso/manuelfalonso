using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Singleton Class to manage AudioMixer volumes.
/// Provides public functions that can be called when these values change.
/// It must receive values from 0.0001 to 1. For example by a slider.
/// Requiered: In the Audio Mixer create two groups. One for Main and other
/// for Music. Music is child of Main. Expose volumne by right clicking
/// Match the name used by the script
/// </summary>
public class AudioMixerConfigurator : MonoBehaviour
{
	[SerializeField]
	private AudioMixer m_Mixer;

	[SerializeField]
	private string m_MixerVarMainVolume = "OverallVolume";

	[SerializeField]
	private string m_MixerVarMusicVolume = "MusicVolume";

	public static AudioMixerConfigurator Instance { get; private set; }

	/// <summary>
	/// We need audio sliders use a value between 0.0001 and 1, but the mixer works in decibels -- by default, -80 to 0.
	/// To convert, we use log10(slider) multiplied by 20. Why 20? because log10(.0001)*20=-80, which is the
	/// bottom range for our mixer, meaning it's disabled.
	/// </summary>
	private const float k_VolumeLog10Multiplier = 20;

	private void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
	public void SetMainVolume(float rawVolume) 
	{
		m_Mixer.SetFloat(m_MixerVarMainVolume, GetVolumeInDecibels(rawVolume));
	}
	
	public void SetMusicVolume(float rawVolume) 
	{
		m_Mixer.SetFloat(m_MixerVarMusicVolume, GetVolumeInDecibels(rawVolume));
	}

	private float GetVolumeInDecibels(float volume)
	{
		if (volume <= 0) // sanity-check
		{
			volume = 0.0001f;
		}
		return Mathf.Log10(volume) * k_VolumeLog10Multiplier;
	}
}
