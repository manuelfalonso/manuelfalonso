using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// To take control of the Text Mesh Pro - Input Field
/// Set True to Control Settings -> Hide Soft Keyboard
/// This script should not work properly with Unity Legacy Input Field
/// TODO: Consider the use or not as a singleton
/// </summary>
public class TouchScreenKeyboardController : MonoBehaviour
{
    [Header("Open Keyboard Parameters")]
    [Tooltip("Text to edit.")]
    [SerializeField] private string _text = string.Empty;
    [Tooltip("Type of keyboard (eg, any text, numbers only, etc).")]
    [SerializeField] private TouchScreenKeyboardType _keyboardType =
        TouchScreenKeyboardType.Default;
    [Tooltip("Is autocorrection applied?")]
    [SerializeField] private bool _autocorrection = true;
    [Tooltip("Can more than one line of text be entered?")]
    [SerializeField] private bool _multiLine = false;
    [Tooltip("Is the text masked (for passwords, etc)?")]
    [SerializeField] private bool _secure = false;
    [Tooltip("Is the keyboard opened in alert mode?")]
    [SerializeField] private bool _alert = false;
    [Tooltip("Text to be used if no other text is present.")]
    [SerializeField] private string _textPlaceholder = string.Empty;
    [Tooltip("How many characters the keyboard input field is limited to. 0 = infinite. (Android and iOS only)")]
    [SerializeField] private int _characterLimit = 0;

    [Header("Static Properties")]
    [Tooltip("Will text input field above the keyboard be hidden when the keyboard is on screen?")]
    [SerializeField] private bool _hideInput = false;

    [Header("Events")]
    public UnityEvent<string> OnValueChanged = new UnityEvent<string>();


    private TouchScreenKeyboard _keyboard = null;


    private void Start()
    {
        TouchScreenKeyboard.hideInput = _hideInput;
    }

    private void LateUpdate()
    {
        SetKeyboardEditText();
    }


    private void SetKeyboardEditText()
    {
        if (!TouchScreenKeyboard.isSupported)
            return;

        if (_keyboard == null)
            return;

        if (!_keyboard.active)
            return;

        // Compares the actual input text of the keyboard with the Open keyboard one
        // Saving this reference let you close the keyboard and open later to continue
        // writing
        if (_keyboard.text != _text)
        {
            _text = _keyboard.text;
            OnValueChanged?.Invoke(_keyboard.text);
        }
    }


    // Called from button or Trigger Event
    public void OpenKeyboard()
    {
        if (!TouchScreenKeyboard.isSupported)
            return;

        if (_keyboard == null || _keyboard.active == false)
        {
            _keyboard = TouchScreenKeyboard.Open(
                _text, 
                _keyboardType, 
                _autocorrection, 
                _multiLine, 
                _secure, 
                _alert, 
                _textPlaceholder, 
                _characterLimit);
            //Debug.Log($"OpenKeyboard");
        }
    }

    /// <summary>
    /// Overload method to open the keyboard with a loaded text 
    /// </summary>
    /// <param name="text"></param>
    public void OpenKeyboard(string text)
    {
        if (!TouchScreenKeyboard.isSupported)
            return;

        if (_keyboard == null || _keyboard.active == false)
        {
            _keyboard = TouchScreenKeyboard.Open(
                text,
                _keyboardType,
                _autocorrection,
                _multiLine,
                _secure,
                _alert,
                _textPlaceholder,
                _characterLimit);
            //Debug.Log($"OpenKeyboard with text");
        }
    }
	
    // Called from button or Trigger Event
    public void CloseKeyboard()
    {
        if (!TouchScreenKeyboard.isSupported)
            return;

        if (_keyboard == null)
            return;

        if (_keyboard.active == true)
        {
            _keyboard.active = false;
            _keyboard = null;
            //Debug.Log($"CloseKeyboard");
        }
    }

    // Called from button or Trigger Event
    public void ResetKeyboardText()
    {
        _text = string.Empty;
        if (_keyboard != null)
            _keyboard.text = string.Empty;
        //Debug.Log($"ResetKeyboardText");
    }


    // FOR DEBUGGING EVENTS
    // Called from button or Trigger Event
    public void DebugLog(string message)
    {
        Debug.Log($"{message}");
    }
}
