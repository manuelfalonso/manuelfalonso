#if UNITY_ADVERTISEMENTS
using UnityEngine;
using UnityEngine.Advertisements;

namespace SombraStudios.Shared.Services.Advertisement
{
    /// <summary>
    /// Class for initializing the Advertisement API.
    /// It's recommended to run it as soon the application start running.
    /// It's needed the Android and/or IOS Game Id obtained from:
    /// Project Settings -> Services -> Ads (after activating Ads in the project)
    /// </summary>
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] string _androidGameId;
        [SerializeField] string _iOsGameId;
        [SerializeField] bool _testMode = true;
        [SerializeField] bool _enablePerPlacementMode = true;
        private string _gameId;

        void Awake()
        {
            InitializeAds();
        }

        public void InitializeAds()
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsGameId
                : _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, _enablePerPlacementMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}
#endif