using SombraStudios.Shared.Editor.SearchTools.AssetOrComponent.Base;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor.SearchTools.AssetOrComponent
{
    /// <summary>
    /// Custom Editor window to search for ParticleSystem components.
    /// </summary>
    public class ParticleSystemComponentSearchWindow : ComponentSearchWindow<ParticleSystem>
    {
        /// <summary>
        /// Opens the ParticleSystem search window from the Unity Editor menu.
        /// </summary>
        [MenuItem("Sombra Studios/Tools/Search Tools/ParticleSystem")]
        public static void OpenWindow()
        {
            GetWindow<ParticleSystemComponentSearchWindow>("Find Particle Systems");
        }


        protected override void Find(GameObject prefab)
        {
            if (prefab == null) { return; }

            var allParticleSystems = prefab.GetComponentsInChildren<ParticleSystem>(_searchInactiveObjects);

            foreach (var particleSystem in allParticleSystems)
            {
                bool alreadyExists = _searchResults.Exists(result => result.Result == particleSystem);
                if (alreadyExists) { continue; }

                // Add additional information if necessary
                var assetInfo = AddAdditionalResultInfo(particleSystem);

                // Add the search result to the list
                _searchResults.Add(assetInfo);
            }
        }
    }
}
