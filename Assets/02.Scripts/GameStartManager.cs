using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelStart;
    [SerializeField]
    private Button textStart;
    [SerializeField]
    private Button textExit;

    void Start()
    {
        textStart.onClick.AddListener(() =>
        {
            GameManager.instance.GameStart();
            panelStart.SetActive(false);

        });
        textExit.onClick.AddListener(GameEnd);
    }

    private void GameEnd()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
