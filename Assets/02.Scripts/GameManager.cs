using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private TextTimerViewer textTimerViewer;
    [SerializeField]
    private Text textAnimTimerText;

    public int time = 60;
    public int destroyedPlate = 0;

    public bool canMiss = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void DestroyPlate()
    {
        instance.destroyedPlate++;
    }

    public void MissPlate()
    {
        if (!canMiss) return;

        if(!(time - 3 <= 0))
        {
            time -= 3;
        }

        textTimerViewer.UpdateTimer(time);
        textAnimTimerText.text = time.ToString();

        textAnimTimerText.gameObject.SetActive(true);
        textAnimTimerText.gameObject.GetComponent<RectTransform>().DOScale(1.5f, 1f).OnComplete(() =>
        {
            textAnimTimerText.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            textAnimTimerText.gameObject.SetActive(false);
        });
    }
}
