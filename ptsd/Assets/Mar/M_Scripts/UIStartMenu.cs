using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartMenu : MonoBehaviour
{
    [SerializeField] Button _startGame;

    void Start()
    {
        _startGame.onClick.AddListener(StartNewScene);
        
    }

    private void StartNewScene()
    {
        ScenesManager.Instance.LoadNextScene();
    }

}
