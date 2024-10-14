using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ToggleBlurOnInput : MonoBehaviour
{
    public PostProcessVolume volume;
    private DepthOfField depthOfField;
    public LayerMask interactableLayer;
    public float maxDistance = 10f;
    private bool isLookingAtObject = false;

    void Start()
    {
        volume.profile.TryGetSettings(out depthOfField);
        depthOfField.active = false; // Ensure Depth of Field starts disabled
        Debug.Log("Depth of Field initialized and set to inactive.");
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.Log("Raycast is running...");

        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
        {
            Debug.Log("Hit detected: " + hit.collider.name);
            if (!isLookingAtObject)
            {
                ToggleBlur(true);
                isLookingAtObject = true;
            }
        }
        else
        {
            Debug.Log("No hit detected.");
            if (isLookingAtObject)
            {
                ToggleBlur(false);
                isLookingAtObject = false;
            }
        }
    }

    void ToggleBlur(bool isActive)
    {
        depthOfField.active = isActive;
        Debug.Log("Blur toggled: " + isActive);
    }
}
