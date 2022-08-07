using System;
using UnityEngine;

/// <summary>
/// Use a collider attached to the object to trigger events in one layer.
/// Events:
/// - onEnterAction
/// - onExitAction
/// </summary>
[RequireComponent(typeof(Collider))]
public class ZoneTrigger : MonoBehaviour
{
    [SerializeField]
    private string triggerLayer;

    public Transform transformInTrigger;
    public bool isInside = false;

    [SerializeField]
    private UnityEngine.Events.UnityEvent<Collider> onEnterAction;
    [SerializeField]
    private UnityEngine.Events.UnityEvent<Collider> onExitAction;

    private int Layer = -1;

    #region Unity Events

    private void Start()
    {
        Layer = LayerMask.NameToLayer(triggerLayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(Layer))
        {
            Debug.LogWarning(
                $"For best performance make collision only for desired layer" +
                $"Project Settings -> Physics/2D -> Layer collision matrix");
            return;
        }

        transformInTrigger = other.transform;
        onEnterAction?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.layer.Equals(Layer))
            return;

        isInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.layer.Equals(Layer))
            return;

        onExitAction?.Invoke(other);
        transformInTrigger = null;
        isInside = false;
    }

    #endregion

    public void SetProximityTriggers(Action<Collider> onEnter, Action<Collider> onExit)
    {
        if (onEnter != null)
        {
            onEnterAction.AddListener((Collider other) => { onEnter(other); });
        }

        if (onExit != null)
        {
            onExitAction.AddListener((Collider other) => { onExit(other); });
        }
    }
}
