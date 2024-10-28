using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAudio : MonoBehaviour
{
    public AudioSource Sound; // The audio source to control
    public float fadeDuration = 1.0f; // Duration of the fade in/out

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeAudio(0, 0.5f, fadeDuration)); 

    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (fadeCoroutine != null)
    //    {
    //        StopCoroutine(fadeCoroutine);
    //    }
    //    fadeCoroutine = StartCoroutine(FadeAudio(1, 0, fadeDuration));
    //}

    // Coroutine to handle the fading process
    private IEnumerator FadeAudio(float startVolume, float targetVolume, float duration)
    {
        float currentTime = 0;

        // Ensure the audio source is playing when fading in
        if (startVolume == 0 && !Sound.isPlaying)
        {
            Sound.Play();
        }

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            Sound.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        // Ensure the target volume is set at the end
        Sound.volume = targetVolume;

        // Stop the audio if fading out
        if (targetVolume == 0)
        {
            Sound.Stop();
        }
    }
}
