#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Utility.PrefabInstantiateOnLoad
{

    /// <summary>
    /// Create a Menu Item for each RuntimeInitializeLoadType scriptable object list
    /// </summary>
    static public class PrefabInstantiateOnLoadEditor
    {
        private const string RESOURCES_FOLDER = "Resources";


        [MenuItem(
            "Assets/Create/Sombra Studios/Utils/Intantiate On Load List/AfterSceneLoad")]
        static void AfterSceneLoad()
        {
            CreateAsset(PrefabInstantiateOnLoad.AFTER_SCENE_LOAD_PATH);
        }

        [MenuItem(
        "Assets/Create/Sombra Studios/Utils/Intantiate On Load List/BeforeSceneLoad")]
        static void BeforeSceneLoad()
        {
            CreateAsset(PrefabInstantiateOnLoad.BEFORE_SCENE_LOAD_PATH);
        }

        [MenuItem(
    "Assets/Create/Sombra Studios/Utils/Intantiate On Load List/AfterAssembliesLoaded")]
        static void AfterAssembliesLoaded()
        {
            CreateAsset(PrefabInstantiateOnLoad.AFTER_ASSEMBLIES_PATH);
        }

        [MenuItem(
    "Assets/Create/Sombra Studios/Utils/Intantiate On Load List/BeforeSplashScreen")]
        static void BeforeSplashScreen()
        {
            CreateAsset(PrefabInstantiateOnLoad.BEFORE_SPLASH_SCREEN_PATH);
        }

        [MenuItem(
    "Assets/Create/Sombra Studios/Utils/Intantiate On Load List/SubsystemRegistration")]
        static void SubsystemRegistration()
        {
            CreateAsset(PrefabInstantiateOnLoad.SUBSYSTEM_REGISTRATION_PATH);
        }


        private static void CreateAsset(string assetName)
        {
            // Create Asset
            var asset =
                ScriptableObject.CreateInstance<PrefabInstantiateOnLoadSO>();

            // Create Path
            var path =
                "Assets"
                + "/"
                + RESOURCES_FOLDER
                + "/"
                + PrefabInstantiateOnLoad.MAIN_PATH
                + "/"
                + assetName
                + ".asset";
            // To avoid Path over writing
            var uniquePath = AssetDatabase.GenerateUniqueAssetPath(path);

            // Create folder if doesn't exist
            CreateFolderStructure();

            // Save Asset
            if (string.IsNullOrEmpty(uniquePath))
                AssetDatabase.CreateAsset(asset, path);
            else
                AssetDatabase.CreateAsset(asset, uniquePath);

            // Reimport the asset after adding an object.
            // Otherwise the change only shows up when saving the project
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(asset));

            // Print the path of the created asset
            Debug.Log($"New Asset created at: {AssetDatabase.GetAssetPath(asset)}");
        }

        private static void CreateFolderStructure()
        {
            var baseFolder = "Assets";
            var folderList =
                new List<string>
                {
                    RESOURCES_FOLDER,
                    PrefabInstantiateOnLoad.MAIN_PATH
                };

            //Check if folder exists with IsValidFolder if it doesn't create it
            foreach (var folder in folderList)
            {
                if (AssetDatabase.IsValidFolder($"{baseFolder}/{folder}"))
                {
                    baseFolder = $"{baseFolder}/{folder}";
                    continue;
                }
                AssetDatabase.CreateFolder(baseFolder, folder);
            }
        }
    }
}
#endif