using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Scenes.SceneAsset
{
    [CreateAssetMenu(fileName = "SceneAssetSO", menuName = "Sombra Studios/Scenes/Scene Asset")]
    public class SceneAssetSO : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.SceneAsset _sceneAsset;
#endif
        public string SceneName;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_sceneAsset != null)
            {
                SceneName = AssetDatabase.GetAssetPath(_sceneAsset);
            }
        }
#endif
    }
}
