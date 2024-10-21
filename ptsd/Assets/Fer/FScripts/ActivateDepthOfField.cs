using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class ToggleBlurOnInput : MonoBehaviour
{
    public PostProcessVolume volume;
    private DepthOfField depthOfField;
    public LayerMask interactableLayer;
    public float maxDistance = 10f;
    private bool isLookingAtObject = false;
    public float transitionDuration = 1f; // Duration for the blur transition

    void Start()
    {
        volume.profile.TryGetSettings(out depthOfField);
        depthOfField.active = true; // Ensure Depth of Field starts enabled
        Debug.Log("Depth of Field initialized and set to active.");
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDistance, Color.red); // Visualize the ray

        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Hit detected: " + hit.collider.name);
            if (!isLookingAtObject)
            {
                StartCoroutine(TransitionBlur(false)); // Disable blur when looking at the object
                isLookingAtObject = true;
            }
        }
        else
        {
            Debug.Log("No hit detected.");
            if (isLookingAtObject)
            {
                StartCoroutine(TransitionBlur(true)); // Enable blur when not looking at the object
                isLookingAtObject = false;
            }
        }
    }

    private IEnumerator TransitionBlur(bool isActive)
    {
        float elapsedTime = 0f;
        bool initialState = depthOfField.active;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            depthOfField.active = Mathf.Lerp(initialState ? 1 : 0, isActive ? 1 : 0, elapsedTime / transitionDuration) > 0.5f;
            yield return null;
        }

        depthOfField.active = isActive;
        Debug.Log("Blur toggled: " + isActive);
    }
}
