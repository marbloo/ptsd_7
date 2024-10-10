using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.InputSystem;

public class ToggleBlurOnInput : MonoBehaviour
{
    public PostProcessVolume volume;
    private DepthOfField depthOfField;
    public InputActionReference activateBlurAction;

    void Start()
    {
        volume.profile.TryGetSettings(out depthOfField);
        activateBlurAction.action.performed += ctx => ToggleBlur();
        depthOfField.active = false;
    }

    void ToggleBlur()
    {
        depthOfField.active = !depthOfField.active;
    }

    void OnEnable()
    {
        activateBlurAction.action.Enable();
    }

    void OnDisable()
    {
        activateBlurAction.action.Disable();
    }
}
