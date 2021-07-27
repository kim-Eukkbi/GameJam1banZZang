using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelStart;
    [SerializeField]
    private GameObject panelStart2;
    [SerializeField]
    private Button textStart;
    [SerializeField]
    private Button textExit;

    void Start()
    {
        if(GameManager.instance.isTest)
        {
            panelStart.SetActive(false);
            panelStart2.SetActive(false);
        }
        else
        {
            textStart.onClick.AddListener(GameStart);
            textExit.onClick.AddListener(GameEnd);
        }
    }

    public void GameStart()
    {
        GameManager.instance.GameStart();
        panelStart.SetActive(false);
        panelStart2.SetActive(false);
    }

    public void GameEnd()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}