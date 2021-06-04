using UnityEngine;

[RequireComponent(typeof(Light))]

public class PingPongLight : MonoBehaviour
{
    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(Time.time, 8);
    }
}