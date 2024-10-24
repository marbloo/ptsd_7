using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour
{
    public int SceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit!");
        if (other.gameObject.CompareTag("Player"))
        {
            PlayScene(SceneIndex);
        }
    }

    private void PlayScene(int sceneIndex)
    {
        SceneTransitionManager.Instance.GoToSceneAsync(sceneIndex);
    }

    private void StartNewScene()
    {
        ScenesManager.Instance.LoadNextScene();
    }


}
