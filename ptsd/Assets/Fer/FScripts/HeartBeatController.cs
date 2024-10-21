using UnityEngine;

public class HeartbeatController : MonoBehaviour
{
    public AudioSource heartbeatAudioSource; // Reference to the AudioSource
    public float targetVolume = 1.0f;         // The final volume you want to reach
    public float fadeDuration = 5.0f;          // Duration to reach the target volume

    private void Start()
    {
        // Start playing the heartbeat sound
        heartbeatAudioSource.Play();
        // Start the volume increase coroutine
        StartCoroutine(IncreaseVolume());
    }

    private System.Collections.IEnumerator IncreaseVolume()
    {
        float startVolume = heartbeatAudioSource.volume; // Get the initial volume
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            // Calculate the new volume
            heartbeatAudioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / fadeDuration);
            yield return null; // Wait for the next frame
        }

        // Ensure the final volume is set to the target volume
        heartbeatAudioSource.volume = targetVolume;
    }
}
