// Credits to Jason Storey
using UnityEngine;

namespace SombraStudios
{

    [AddComponentMenu("_SombraStudios/Services/Logger")]
    public class Logger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private bool _showLogs = false;
        [SerializeField]
        private string _prefix = string.Empty;
        [SerializeField]
        private Color _prefixColor = Color.white;

        private string _hexColor = string.Empty;


        private void OnValidate()
        {
            _hexColor = "#" + ColorUtility.ToHtmlStringRGBA(_prefixColor);
        }


        public void Log(object message, Object sender)
        {
            if (!_showLogs) return;
            Debug.Log($"<color={_hexColor}>{_prefix}</color> {message}", sender);
        }
    }
}
