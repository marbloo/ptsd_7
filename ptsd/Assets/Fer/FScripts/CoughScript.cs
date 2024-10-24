using System.Collections;
using UnityEngine;

public class DelayedSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip sound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       

        StartCoroutine(PlaySoundAfterDelay(60f)); // Start the coroutine to play sound after 60 seconds
    }

    private IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(sound); // Play the sound
       
    }
}
