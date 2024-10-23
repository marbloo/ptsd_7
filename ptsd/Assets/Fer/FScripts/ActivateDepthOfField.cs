using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro; // Ensure this namespace is included for TextMeshPro

public class PaperInteraction : MonoBehaviour
{
    public float rayDistance = 10f;  // Maximum distance for the ray
    public LayerMask interactableLayer;  // Layer for the paper

    // Text writing components
    public TextMeshPro paperText;  // Reference to the TextMeshPro component on the paper
    public string fullText = "This is the text that will be written.";  // The full text to write
    public float writingSpeed = 0.1f;  // Delay between each letter
    private int currentCharIndex = 0;  // Index of the character currently being written
    private Coroutine writingCoroutine;  // Reference to the writing coroutine

    // Post-processing components for blur
    public PostProcessVolume volume;
    private DepthOfField depthOfField;
    private bool isLookingAtPaper = false;  // Track if the player is looking at the paper
    public float transitionDuration = 1f;  // Duration for the blur transition

    void Start()
    {
        // Initialize Depth of Field
        volume.profile.TryGetSettings(out depthOfField);
        depthOfField.active = true;  // Ensure Depth of Field starts enabled
        Debug.Log("Depth of Field initialized and set to active.");
    }

    void Update()
    {
        // Raycast setup using Camera.main directly
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * rayDistance, Color.red);  // Visualize the ray

        // Check for interaction with the paper
        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Hit detected: " + hit.collider.name);

            if (hit.collider.CompareTag("Paper"))  // Ensure the hit object is the paper
            {
                // Start writing text on the paper if not already doing so
                if (writingCoroutine == null)
                {
                    writingCoroutine = StartCoroutine(WriteText());
                }

                // Disable blur when looking at the paper
                if (!isLookingAtPaper)
                {
                    StartCoroutine(TransitionBlur(false));  // Disable blur
                    isLookingAtPaper = true;
                }
            }
        }
        else  // If no hit detected, enable blur
        {
            Debug.Log("No paper hit detected.");
            if (isLookingAtPaper)  // Enable blur when not looking at the paper
            {
                StartCoroutine(TransitionBlur(true));  // Enable blur
                isLookingAtPaper = false;

                // Stop writing if currently writing
                if (writingCoroutine != null)
                {
                    StopCoroutine(writingCoroutine);  // Stop the writing coroutine
                    writingCoroutine = null;  // Clear the reference
                }
            }
        }
    }

    // Coroutine for text writing
    private IEnumerator WriteText()
    {
        // Write text from the current character index
        while (currentCharIndex < fullText.Length)
        {
            paperText.text = fullText.Substring(0, currentCharIndex + 1);  // Update the text to show current progress
            currentCharIndex++;  // Move to the next character
            yield return new WaitForSeconds(writingSpeed);  // Wait before writing the next letter
        }

        writingCoroutine = null;  // Clear the reference when done
    }

    // Coroutine for transitioning the blur (Depth of Field effect)
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
