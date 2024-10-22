using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LightFlicker : MonoBehaviour
{
    public Light flickerLight;              // Reference to the light you want to flicker
    public Renderer lightRenderer;          // Reference to the cube's renderer (which has the material)
    public Color lightOnColor = Color.white;  // Color of the material when the light is on
    public Color lightOffColor = Color.gray;  // Color of the material when the light is off
    public float flickerDuration = 5f;      // Duration of the flicker effect
    public float flickerSpeed = 0.1f;       // How fast the light flickers on/off
    public float startDelay = 10f;          // Delay in seconds before flickering starts
    public AudioSource soundEffect;         // Reference to the sound effect
    public PostProcessVolume postProcessVolume; // Reference to the Post Process Volume
    private DepthOfField depthOfField;      // Reference to the Depth of Field effect

    private bool isFlickering = false;      // Track if the light is currently flickering
    private Material cubeMaterial;          // Reference to the material

    void Start()
    {
        // Get the material from the renderer
        cubeMaterial = lightRenderer.material;

        // Get the Depth of Field from the Post Process Volume
        postProcessVolume.profile.TryGetSettings(out depthOfField);

        // Start coroutine with the delay
        StartCoroutine(StartAfterDelay());
    }

    IEnumerator StartAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(startDelay);

        // Start sound effect if available
        if (soundEffect != null)
        {
            soundEffect.Play();
        }

        // Start the flicker coroutine
        StartCoroutine(FlickerLightAndMaterial());
    }

    IEnumerator FlickerLightAndMaterial()
    {
        isFlickering = true;

        // Activate blur effect
        if (depthOfField != null)
        {
            depthOfField.active = true; // Enable the blur effect
        }

        // Flicker for the specified duration
        for (float t = 0; t < flickerDuration; t += flickerSpeed)
        {
            // Randomly turn the light on or off
            bool lightIsOn = Random.value > 0.5f;

            // Toggle the light
            flickerLight.enabled = lightIsOn;

            // Toggle the material color
            if (lightIsOn)
            {
                cubeMaterial.color = lightOnColor;  // Set material color when light is on
            }
            else
            {
                cubeMaterial.color = lightOffColor;  // Set material color when light is off
            }

            // Wait for the next flicker cycle
            yield return new WaitForSeconds(flickerSpeed);
        }

        // Ensure the light and material color are on after flickering ends
        flickerLight.enabled = true;
        cubeMaterial.color = lightOnColor;

        // Deactivate blur effect after flickering
        if (depthOfField != null)
        {
            depthOfField.active = false; // Disable the blur effect
        }

        isFlickering = false;
    }

    // Optional: Method to trigger flickering externally (e.g., based on events)
    public void TriggerFlicker(float customDuration = 0)
    {
        if (!isFlickering)
        {
            flickerDuration = customDuration > 0 ? customDuration : flickerDuration;
            StartCoroutine(FlickerLightAndMaterial());
        }
    }
}
