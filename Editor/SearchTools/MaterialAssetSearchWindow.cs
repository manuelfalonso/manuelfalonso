using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor.SearchTools
{
    /// <summary>  
    /// Custom Editor window to search for Material assets.  
    /// </summary> 
    public class MaterialsAssetSearchWindow : AssetSearchWindow<Material>
    {
        /// <summary>  
        /// Class to hold additional information about the material search results.  
        /// </summary>  
        protected class MaterialAdditionalInfo : ResultsAdditionalInfo
        {
            /// <summary>  
            /// The name of the shader used by the material.  
            /// </summary>  
            public string ShaderName;
        }


        /// <summary>  
        /// Opens the Material search window from the Unity Editor menu.  
        /// </summary>  
        [MenuItem("Sombra Studios/Tools/Search Tools/Material")]
        public static void OpenWindow()
        {
            GetWindow<MaterialsAssetSearchWindow>("Find Materials");
        }


        protected override void Find(GameObject go)
        {
            if (go == null) { return; }

            var allRenderers = go.GetComponentsInChildren<Renderer>(_searchInactiveObjects);

            foreach (var renderer in allRenderers)
            {
                foreach (var material in renderer.sharedMaterials)
                {
                    // Verify if the material is already in the search results
                    bool alreadyExists = _searchResults.Exists(result => result.Result == material);
                    if (alreadyExists) { continue; }

                    // Add additional information if necessary
                    var assetInfo = AddAdditionalResultInfo(material);//, assetInfo);

                    // Add the search result to the list
                    _searchResults.Add(assetInfo);
                }
            }
        }

        protected override SearchResults AddAdditionalResultInfo(Material asset)
        {
            var test = new MaterialAdditionalInfo()
            {
                ShaderName = asset.shader.name
            };

            return new SearchResults()
            {
                Result = asset,
                AdditionalInfos = new List<ResultsAdditionalInfo>()
                {
                    test
                }
            };
        }

        protected override void ShowAdditionalResultInfo(ResultsAdditionalInfo assetInfo)
        {
            if (assetInfo == null) { return; }
            var info = (MaterialAdditionalInfo)assetInfo;
            EditorGUILayout.LabelField(info.ShaderName);
        }
    }
}
