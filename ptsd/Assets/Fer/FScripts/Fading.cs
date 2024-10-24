using System.Collections;
using UnityEngine;

public class Fading : MonoBehaviour
{
    public bool fadeOnStart = true; // Determines if the fade should happen on start
    public float fadeDuration = 2f; // Duration for the fade
    public Color fadeColor = Color.black; // Color of the fade
    private Renderer quadRenderer; // Reference to the Quad's Renderer

    void Start()
    {
        quadRenderer = GetComponent<Renderer>(); // Get the Renderer component attached to the Quad
        if (quadRenderer == null)
        {
            Debug.LogError("Renderer component is missing from this GameObject.");
            return;
        }

        if (fadeOnStart)
        {
            StartCoroutine(FadeRoutine(1f, 0f, fadeDuration)); // Start fading in when the scene starts
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(1f, 0f, fadeDuration)); // Fades in from opaque to transparent
    }

    public void FadeOut()
    {
        StartCoroutine(FadeRoutine(0f, 1f, fadeDuration)); // Fades out from transparent to opaque
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;
        // Create a new color with the specified fade color and alpha values
        Color color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, startAlpha);
        quadRenderer.material.color = color; // Set initial color

        while (timer < duration)
        {
            // Interpolate the alpha value over time
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
            quadRenderer.material.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha); // Apply the new alpha
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final alpha is set
        quadRenderer.material.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, endAlpha);
    }
}
