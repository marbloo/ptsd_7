using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class InstructionDeactivator : MonoBehaviour
{
    public float duration;
    public PostProcessVolume postProcessVolume; 
  

   
    void Start()
    {
        if (postProcessVolume != null)
        {
            postProcessVolume.enabled = false;
        }
        StartCoroutine(deactivate());
    }

    // Update is called once per frame
    IEnumerator deactivate()
    {

        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);

        if (postProcessVolume != null)
        {
            postProcessVolume.enabled = true;
        }
        gameObject.SetActive(false);

    }
}
