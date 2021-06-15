using UnityEngine;

/// <summary>
/// Persistent data saving using PlayerPrefs class
/// </summary>
public class SaveInPlayerPrefs : MonoBehaviour
{
    // Click Play, modify in the inspector the string and stop.
    // Run again and you should see the string modified.
    public string test;

    void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("json test"), this);
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("json test", JsonUtility.ToJson(this, true));
        PlayerPrefs.Save();
    }
}
