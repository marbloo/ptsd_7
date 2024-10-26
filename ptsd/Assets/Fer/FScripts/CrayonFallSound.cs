using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR.Interaction.Toolkit;
public class CrayonFallSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip sound;
    public PostProcessVolume volume;
    private DepthOfField depthOfField;
    public float blurDuration = 2f; // Duration to keep the blur effect active
    private bool soundPlayed = false; // Flag to check if sound has been played
    public float delaySound = 20f; // Delay before sound is played
    public PenMovement penMovement; // Reference to the PenMovement script

    public HapticHands haptics;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from this GameObject.");
            return;
        }
        if (sound == null)
        {
            Debug.LogError("AudioClip is not assigned.");
            return;
        }
        // Check if Depth of Field is available in the PostProcess profile
        if (volume.profile.TryGetSettings(out depthOfField))
        {
            depthOfField.active = false; // Ensure Depth of Field starts disabled
        }
        else
        {
            Debug.LogWarning("Depth of Field not found in Post Process Volume.");
        }
    }

  

    void Update()
    {
        // Automatically play sound and activate blur if Depth of Field is inactive
        if (!depthOfField.active && !soundPlayed)
        {
            Invoke(nameof(PlaySoundAndActivateBlur), delaySound); // Use Invoke to delay the method call
            soundPlayed = true; // Set the flag to true to prevent further sound playing
        }
    }

    void PlaySoundAndActivateBlur()
    {
        audioSource.PlayOneShot(sound);
        depthOfField.active = true; // Activate blur effect
        penMovement.MovePen(); // Trigger the pen movement
        StartCoroutine(DeactivateBlurAfterDuration()); // Start coroutine to deactivate blur
        haptics.TriggerBothHaptics();


    }

     IEnumerator DeactivateBlurAfterDuration()
    {
        yield return new WaitForSeconds(blurDuration);
        depthOfField.active = false; // Deactivate blur effect
        Debug.Log("Blur deactivated after duration.");
      
    }
}
