using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing; // Import Post-Processing namespace

public class DelayedSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip sound;
    public float bDuration = 2f;

    public HapticHands hands;
   

    public PostProcessVolume postProcessVolume;  // Reference to the Post-Processing Volume
    private DepthOfField depthOfField;           // Depth of Field effect

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Get the Depth of Field effect from the Post-Processing Volume
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out depthOfField);
        }

        StartCoroutine(PlaySoundAfterDelay(40f)); // Start the coroutine to play sound after 40 seconds
    }

    private IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(sound); // Play the sound
        hands.TriggerBothHaptics();     // Trigger haptics

        // Activate Depth of Field if available
        if (depthOfField != null)
        {
            depthOfField.active = true; // Enable Depth of Field
        }
        StartCoroutine(DeactivateBlurAfterDuration());
    }
    IEnumerator DeactivateBlurAfterDuration()
    {
        yield return new WaitForSeconds(bDuration);
        depthOfField.active = false; // Deactivate blur effect
       

    }
}
