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
    private GameObject panelStartInvisible;
    [SerializeField]
    private Button textStart;
    [SerializeField]
    private Button textExit;

    void Start()
    {
        if(GameManager.instance.isTest)
        {
            panelStart.SetActive(false);
            panelStartInvisible.SetActive(false);
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
        StartCoroutine(LateSetActiveFalse(panelStart));
        panelStartInvisible.SetActive(false);
        GameManager.instance.player.vCams[4].gameObject.SetActive(false);
        GameManager.instance.player.vCams[0].gameObject.SetActive(true);
    }

    private IEnumerator LateSetActiveFalse(GameObject gameObject)
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
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
