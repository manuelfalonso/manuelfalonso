using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Unity Pointer handlers interfaces for UI mouse events. 
/// </summary>
public class PointerHandlers : MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerMoveHandler,
    IPointerUpHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Call if the click start and finish over an UI object
        Debug.LogWarning("OnPointerClick");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Call if the click start over a UI object
        Debug.LogWarning("OnPointerDown");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Begin to hover a UI object
        Debug.LogWarning("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Stop to hover a UI Object
        Debug.LogWarning("OnPointerExit");
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        // Called on every frame over a UI object
        Debug.LogWarning("OnPointerMove");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Call if the click start over a UI object,
        // independently where you release the click.
        Debug.LogWarning("OnPointerUp");
    }
}
