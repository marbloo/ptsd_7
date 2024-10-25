using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door") || other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(clip);
        }
    }

}
