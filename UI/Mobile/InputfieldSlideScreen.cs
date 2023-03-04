using TMPro;
using UnityEngine;

/// <summary>
/// When the Touch Keyboard is active and the assigned input field is focused,
/// the assigned rect transform will adjust Y position to be on top of the keyboard.
/// </summary>
public class InputfieldSlideScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField Input = default;
    [SerializeField]
    private RectTransform RectToSlide = default;

    private Vector2 StartAnchoredPosition;

    private void Start()
    {
        StartAnchoredPosition = RectToSlide.anchoredPosition;
    }

#if UNITY_IOS || UNITY_ANDROID
    void LateUpdate()
    {
        if (Input.isFocused && TouchScreenKeyboard.visible)
        {
            float keyboardHeight = TouchScreenKeyboard.area.height;
            float heightPercentOfKeyboard = keyboardHeight / Screen.height * 100f;
            float newYPos = RectToSlide.rect.height / 100f * heightPercentOfKeyboard;

            RectToSlide.anchoredPosition = new Vector2(0, newYPos);
        }
        else
        {
            RectToSlide.anchoredPosition = StartAnchoredPosition;
        }
    }
#endif
}
