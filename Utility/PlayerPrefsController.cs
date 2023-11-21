using UnityEngine;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Class with static methods for saving and loading data from PlayerPrefs
    /// </summary>
    public class PlayerPrefsController : MonoBehaviour
    {
        // Click Play, modify in the inspector the string and stop.
        // Run again and you should see the string modified.
        public string test;

        // Keys
        const string MASTER_VOLUME_KEY = "master volume";
        const string DIFFICULTY_KEY = "difficulty";

        // Volume
        const float MIN_VOLUME = 0f;
        const float MAX_VOLUME = 1f;

        // Difficulty
        const float MIN_DIFFICULTY = 0f;
        const float MAX_DIFFICULTY = 2f;

        // Saving a class as Json string
        void OnEnable()
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("json test"), this);
        }
        // Loading a class as Json string
        void OnDisable()
        {
            PlayerPrefs.SetString("json test", JsonUtility.ToJson(this, true));
            PlayerPrefs.Save();
        }

        public static void SetMasterVolume(float volume)
        {
            if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
            {
                Debug.Log("Master volume set to " + volume);
                PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
            }
            else
            {
                Debug.LogError("Master volume is out of range");
            }
        }

        public static float GetMasterVolume()
        {
            return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
        }

        public static void SetDifficulty(float difficulty)
        {
            if (difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
            {
                PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
            }
            else
            {
                Debug.LogError("Difficulty setting is not in range");
            }
        }

        public static float GetDifficulty()
        {
            return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
        }
    }
}
