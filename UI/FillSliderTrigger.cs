using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Triggers an event when a slider bar is filled using an input
/// Compatibility with both, legacy and the new Input System
/// </summary>
public class FillSliderTrigger : MonoBehaviour
{
    [SerializeField]
    private Slider _slider = default(Slider);

    [Header("Input")]
#if ENABLE_INPUT_SYSTEM
    [SerializeField]
    private InputAction _inputAction = default(InputAction);
#elif ENABLE_LEGACY_INPUT_MANAGER
    [SerializeField]
    private KeyCode _legacyInputKey = default(KeyCode);
#endif

    [Header("Config")]
    [SerializeField]
    private float _fillSpeed = 1.0f;
    [SerializeField]
    private bool _unfillActive = false;
    [SerializeField]
    private float _unfillSpeed = 0.1f;

    [Header("Events")]
    public UnityEvent OnSliderFilled = new UnityEvent();


    private bool _isFilled = false;


#if ENABLE_INPUT_SYSTEM
    private void OnEnable()
    {
        _inputAction.Enable();
    }
#endif

    void Update()
    {
        CheckInput();
    }

#if ENABLE_INPUT_SYSTEM
    private void OnDisable()
    {
        _inputAction?.Disable();
    }
#endif


    private void CheckInput()
    {
        if (_isFilled)
            return;

        // Fill
#if ENABLE_INPUT_SYSTEM
        // New Input System
        if (_inputAction.inProgress)
            _slider.value += Time.deltaTime * _fillSpeed;
#elif ENABLE_LEGACY_INPUT_MANAGER
        // Old Input System
        if(Input.GetKey(_legacyInputKey))
            _slider.value += Time.deltaTime * _fillSpeed;
#endif
        // Unfill
        else if (_unfillActive)
            _slider.value -= Time.deltaTime * _unfillSpeed;

        // Check complete event
        if (!_isFilled && _slider.value >= 1)
        {
            _isFilled = true;
            OnSliderFilled?.Invoke();
        }
    }
}
