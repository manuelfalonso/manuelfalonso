using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// IsPointerOverGameObject function to check if mouse is over GUI
/// </summary>
public class MouseOverGUI : MonoBehaviour
{
    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
            }
        }
    }
}
