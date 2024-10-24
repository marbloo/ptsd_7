using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScene : MonoBehaviour
{
    public int SceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayScene(2);
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
