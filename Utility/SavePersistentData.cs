using System.IO;
using UnityEngine;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Example Class for saving persistent data during runs, re-installing or updating.
    /// Save method can be use at inicialization and load at quit method.
    /// </summary>
    public class SavePersistentData : MonoBehaviour
    {
        public Color TeamColor;

        private void OnEnable()
        {
            LoadColor();
        }

        private void OnDisable()
        {
            SaveColor();
        }

        /// <summary>
        /// Example data Class. Data need to be serealizable type. Not primitive addmited.
        /// </summary>
        [System.Serializable]
        class SaveData
        {
            public Color TeamColor;
        }

        /// <summary>
        /// Save serealizable data in a json file in a persistent file.
        /// </summary>
        public void SaveColor()
        {
            SaveData data = new SaveData();
            data.TeamColor = TeamColor;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }

        /// <summary>
        /// Load serealizable data from a json file in a persistent file.
        /// </summary>
        public void LoadColor()
        {
            string path = Application.persistentDataPath + "/savefile.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                TeamColor = data.TeamColor;
            }
        }
    }
}
