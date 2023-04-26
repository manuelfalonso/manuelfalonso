using UnityEngine;

namespace SombraStudios.Utils
{

    /// <summary>
    /// Creates on intialize a list of prefabs saved on Resources folder
    /// It can differentiate between all RuntimeInitializeLoadType
    /// Each corresponding asset list can be created in menu:
    /// Assets/Create/Sombra Studios/Utils/Intantiate On Load List
    /// Documentation:
    /// https://docs.unity3d.com/ScriptReference/RuntimeInitializeLoadType.html
    /// </summary>
    public class PrefabInstantiateOnLoad : MonoBehaviour
    {
        public const string MAIN_PATH = "Instantiate Prefab Lists";
        // Assets names
        public const string AFTER_SCENE_LOAD_PATH = "AfterSceneLoad List";
        public const string BEFORE_SCENE_LOAD_PATH = "BeforeSceneLoad List";
        public const string AFTER_ASSEMBLIES_PATH = "AfterAssembliesLoaded List";
        public const string BEFORE_SPLASH_SCREEN_PATH = "BeforeSplashScreen List";
        public const string SUBSYSTEM_REGISTRATION_PATH = "SubsystemRegistration List";


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void InstantiatePrefabListAfterSceneLoad()
        {
            InstantiatePrefabList(AFTER_SCENE_LOAD_PATH);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InstantiatePrefabListBeforeSceneLoad()
        {
            InstantiatePrefabList(BEFORE_SCENE_LOAD_PATH);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void InstantiatePrefabListAfterAssembliesLoaded()
        {
            InstantiatePrefabList(AFTER_ASSEMBLIES_PATH);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void InstantiatePrefabListBeforeSplashScreen()
        {
            InstantiatePrefabList(BEFORE_SPLASH_SCREEN_PATH);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InstantiatePrefabListSubsystemRegistration()
        {
            InstantiatePrefabList(SUBSYSTEM_REGISTRATION_PATH);
        }


        private static void InstantiatePrefabList(string scriptableObjectName)
        {
            var path = MAIN_PATH + "/" + scriptableObjectName;
            var prefabListSO =
                Resources.Load<PrefabInstantiateOnLoadSO>(path);

            if (prefabListSO == null) { return; }
            if (prefabListSO.PrefabList == null) return;
            if (prefabListSO.PrefabList.Count == 0) return;

            foreach (var item in prefabListSO.PrefabList)
            {
                if (item == null) continue;

                Instantiate(item);
            }
        }
    }
}
