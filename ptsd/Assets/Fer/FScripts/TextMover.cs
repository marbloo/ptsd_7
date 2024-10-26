using System.Collections;
using UnityEngine;

public class ShakeOnCollision : MonoBehaviour
{
    public Transform[] textTransforms; // Array to hold all the Text Transforms
    public float shakeDuration = 1f;
    public float shakeMagnitude = 0.02f; // Reduced magnitude for subtle shaking

    private bool isShaking = false;
    private Vector3[] originalPositions;

    void Start()
    {
        // Store the initial positions of all text elements
        originalPositions = new Vector3[textTransforms.Length];
        for (int i = 0; i < textTransforms.Length; i++)
        {
            originalPositions[i] = textTransforms[i].localPosition;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Paper"))
        {
            Debug.Log("Shake");
            if (!isShaking)
            {
                StartCoroutine(ShakeText());
            }
        }
    }

    private IEnumerator ShakeText()
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            for (int i = 0; i < textTransforms.Length; i++)
            {
                float x = originalPositions[i].x + Random.Range(-1f, 1f) * shakeMagnitude;
                float y = originalPositions[i].y + Random.Range(-1f, 1f) * shakeMagnitude;
                textTransforms[i].localPosition = new Vector3(x, y, originalPositions[i].z);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset all text positions to their original values
        for (int i = 0; i < textTransforms.Length; i++)
        {
            textTransforms[i].localPosition = originalPositions[i];
        }

        isShaking = false;
    }
}
