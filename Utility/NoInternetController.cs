using SombraStudios.Shared.Patterns.Creational.Singleton;
using System;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Utility
{
    public class NoInternetController : Singleton<NoInternetController>
    {
        private Action<bool> OnInternetUpdateStatus = null;

        private float _secondsToWaitToCheck = 5f;
        // Google DNS
        private string _pingIP = "8.8.8.8";

        void Start()
        {
            StartCoroutine(CheckInternetConnection((isConnected) =>
            {
                OnInternetUpdateStatus?.Invoke(isConnected);
            }));
        }

        IEnumerator CheckInternetConnection(Action<bool> callBack)
        {
            yield return new WaitForSeconds(_secondsToWaitToCheck);

            while (true)
            {
                // Ping
                var ping = new Ping(_pingIP);

                yield return new WaitForSeconds(_secondsToWaitToCheck);

                callBack(ping.isDone);
            }
        }
    }
}
